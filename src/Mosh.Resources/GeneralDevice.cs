using Microsoft.Extensions.Logging;
using Moryx.AbstractionLayer.Drivers.Message;
using Moryx.AbstractionLayer.Resources;
using Moryx.ProcessData;
using Moryx.Protocols.Shelly;
using Moryx.Serialization;
using Mosh.Capabilities;
using Mosh.Protocols.Shelly;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mosh.Resources
{
    [ResourceAvailableAs(typeof(IProcessDataPublisher))]
    public class GeneralDevice : PublicResource, IProcessDataPublisher
    {
        [ResourceReference(ResourceRelationType.Extension)]
        public IMessageDriver<object> MessageDriver { get; set; }

        [DataMember, EntrySerialize]
        public string Prefix { get; set; }

        [DataMember, EntrySerialize]
        public int Channel { get; set; }

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
                case IPowerStatus powerStatus when powerStatus.Prefix == Prefix && powerStatus.Channel == Channel:
                    var measurement = new Measurement("device_power");
                    measurement.Add(new DataTag("device", Name));
                    measurement.Add(new DataField("power", powerStatus.CurrentPower));
                    ProcessDataOccurred?.Invoke(this, measurement);
                    break;
            }
        }

        public event EventHandler<Measurement> ProcessDataOccurred;
    }
}
