using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mosh.Protocols.Shelly
{
    /// <summary>
    /// Message received on TOPIC_PREFIX/status/{Component:Id}
    /// </summary>
    [DataContract]
    public class ShellyStatusMessage
    {
        public ShellyStatusMessage()
        {
        }

        public string? Prefix { get; set; }

        public string ComponentType { get; set; } = string.Empty;

        public int ComponentId { get; set; }

        public string ComponentTopic
        {
            get => string.Empty; // We do not publish this
            set
            {
                var parts = value.Split(':');
                ComponentType = parts[0];
                if (parts.Length >= 2 && int.TryParse(parts[1], out var id))
                    ComponentId = id;
            }
        }

        [DataMember(Name = "state")]
        public bool State { get; set; }

        [DataMember(Name = "output")]
        public bool Output { get; set; }

        [DataMember(Name = "apower")]
        public double ActivePower { get; set; }

        public const string SwitchType = "switch";

        public const string InputType = "input";

        public const string PowerMeterType = "pm1";
    }
}
