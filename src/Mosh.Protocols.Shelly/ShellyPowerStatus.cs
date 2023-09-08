using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mosh.Protocols.Shelly
{
    /// <summary>
    /// Message received on TOPIC_PREFIX/status/{ChannelTopic}
    /// </summary>
    [DataContract]
    public class ShellyPowerStatus : IPowerStatus
    {
        public ShellyPowerStatus()
        {
        }

        public string? Prefix { get; set; }

        public int Channel { get; set; }

        public string ChannelTopic
        {
            get => $"pm1:{Channel}";
            set => Channel = int.Parse(value.Split(':')[1]);
        }

        [DataMember(Name = "apower")]
        public double CurrentPower { get; set; }
    }
}
