using Moryx.AbstractionLayer.Drivers.Message;
using Moryx.AbstractionLayer.Resources;
using Moryx.ProcessData;
using Moryx.Serialization;
using Mosh.Protocols.Shelly;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Mosh.Resources
{
    public class ShellyInputDevice : PublicResource, IInputDevice
    {
        [ResourceReference(ResourceRelationType.Driver)]
        public IMessageDriver<object> MessageDriver { get; set; }

        [DataMember, EntrySerialize]
        public string Prefix { get; set; }

        [DataMember, EntrySerialize]
        public int ComponentId { get; set; }

        private DateTime _lastUpdate;

        [EntrySerialize, ReadOnly(true)]
        public string LastUpdate => _lastUpdate.ToString("yy-MM-dd HH:mm");

        [EntrySerialize, ReadOnly(true)]
        public double Value { get; set; }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            if (MessageDriver != null)
            {
                MessageDriver.Received += OnMessageReceived;
            }
        }

        private void OnMessageReceived(object sender, object message)
        {
            switch (message)
            {
                case ShellyEventMessage eventMsg when eventMsg.Prefix == Prefix:
                    if ((eventMsg.Parameters?.Events?.Length ?? 0) <= 0)
                        return;

                    switch(eventMsg.Parameters.Events[0].Event)
                    {
                        case "single_push":
                            Value = InputDeviceValues.SinglePush;
                            break;
                        case "double_push":
                            Value = InputDeviceValues.DoublePush;
                            break;
                        case "triple_push":
                            Value = InputDeviceValues.TriplePush;
                            break;
                    }

                    _lastUpdate = DateTime.Now;
                    break;
            }
        }

        public event EventHandler ValueChanged;
    }
}
