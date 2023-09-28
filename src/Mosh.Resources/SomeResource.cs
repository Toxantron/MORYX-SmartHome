using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using Microsoft.Extensions.Logging;
using Moryx.AbstractionLayer.Drivers.Message;
using Moryx.AbstractionLayer.Resources;
using Moryx.Protocols.Shelly;
using Moryx.Serialization;
using Mosh.Capabilities;
using Mosh.Protocols.Shelly;

namespace Mosh.Resources
{
    [ResourceRegistration] // Only necessary for dependency injection like logging or parallel operations
    public class SomeResource : PublicResource, ISomeResource
    {
        [DataMember, EntrySerialize]
        [Description("Configured value for the capabilities")]
        public int Value { get; set; }

        [ResourceReference(ResourceRelationType.Extension)]
        public IMessageDriver<object> MessageDriver { get; set; }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            Capabilities = new SomeCapabilities{Value = Value};

            if(MessageDriver != null)
            {
                MessageDriver.Received += OnMessageReceived;
            }
        }

        private void OnMessageReceived(object sender, object message)
        {
            switch (message)
            {
                case ShellyStatusMessage status:
                    Logger.LogInformation("Shelly output {0}:{1} in state {2} with power {3}", status.Prefix, status.ComponentId, status.State, status.ActivePower);
                    break;
                case ShellyInputEvent input:
                    Logger.LogInformation("Shelly input {0}:{1} in state {2}", input.Prefix, input.Input, input.State);
                    break;
            }
        }

        [EntrySerialize]
        public void SetOutput(string name)
        {
            MessageDriver.Send(new ShellyCommandMessage(name, ShellyCommand.Toggle));
        }
    }
}