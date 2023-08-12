using Moryx.Communication;
using Moryx.Protocols.Shelly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mosh.Protocols.Shelly
{
    /// <summary>
    /// Base class for all shelly MQTT Control messages
    /// </summary>
    public abstract class ShellyTopicMessage
    {
        public ShellyTopicMessage()
        {
        }

        protected ShellyTopicMessage(string prefix)
        {
            Prefix = prefix;
        }

        public string Prefix { get; private set; }

        public int Switch { get; set; }

        public string SwitchTopic
        {
            get => $"switch:{Switch}";
            set => Switch = int.Parse(value.Split(':')[1]);
        }
    }
}
