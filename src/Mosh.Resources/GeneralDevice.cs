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

        [EntrySerialize, ReadOnly(true)]
        public double CurrentPower { get; set; }

        private DateTime _lastUpdate;
        [EntrySerialize, ReadOnly(true)]
        public string LastUpdate => _lastUpdate.ToString("yy-MM-dd HH:mm");

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
                    // Avoid redundant measurements. Difference of less than 1W is not reported within a 15min time frame
                    if (Math.Abs(CurrentPower - powerStatus.CurrentPower) < 1 
                        && (DateTime.Now - _lastUpdate).TotalMinutes < 15)
                        return;

                    var measurement = new Measurement("devices");
                    measurement.Add(new DataTag("device", Name));
                    measurement.Add(new DataField("power", CurrentPower = powerStatus.CurrentPower));
                    ProcessDataOccurred?.Invoke(this, measurement);

                    _lastUpdate = DateTime.Now;
                    break;
            }
        }

        public event EventHandler<Measurement> ProcessDataOccurred;
    }
}
