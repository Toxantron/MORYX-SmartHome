﻿using Microsoft.Extensions.Logging;
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
        public int ComponentId { get; set; }

        [EntrySerialize, ReadOnly(true)]
        public double ActivePower { get; set; }

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

        protected override void OnDispose()
        {
            if (MessageDriver != null)
            {
                MessageDriver.Received -= OnMessageReceived;
            }

            base.OnDispose();
        }

        private void OnMessageReceived(object sender, object message)
        {
            switch (message)
            {
                case ShellyStatusMessage status when status.Prefix == Prefix && status.ComponentId == ComponentId:
                    // Avoid redundant measurements. Difference of less than 1W is not reported within a 15min time frame
                    if (Math.Abs(ActivePower - status.ActivePower) < 1 
                        && (DateTime.Now - _lastUpdate).TotalMinutes < 15)
                        return;

                    var measurement = new Measurement("devices");

                    measurement.Add(new DataTag("name", Name));
                    measurement.Add(new DataField("voltage", status.Voltage));
                    measurement.Add(new DataField("power", ActivePower = status.ActivePower));

                    // Recursively iterate hierarchy to determine room, floor and building
                    Resource current = this;
                    while (current.Parent is BuildingStructure building)
                    {
                        measurement.Add(new DataTag(building.ElementType.ToString("G").ToLower(), building.Name));
                        current = building;
                    }

                    ProcessDataOccurred?.Invoke(this, measurement);

                    _lastUpdate = DateTime.Now;
                    break;
            }
        }

        public event EventHandler<Measurement> ProcessDataOccurred;
    }
}
