using Moryx.Protocols.Shelly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mosh.Protocols.Shelly
{
    /// <summary>
    /// Message received on TOPIC_PREFIX/status/{SwitchTopic}
    /// </summary>
    [DataContract]
    public class ShellySwitchStatus : IPowerStatus
    {
        public ShellySwitchStatus()
        {
        }

        public string? Prefix { get; set; }

        public int Switch { get; set; }

        public int Channel => Switch;

        public string SwitchTopic
        {
            get => $"switch:{Switch}";
            set => Switch = int.Parse(value.Split(':')[1]);
        }

        [DataMember(Name = "output")]
        public bool State { get; set; }

        [DataMember(Name = "apower")]
        public double CurrentPower { get; set; }

    }
}
