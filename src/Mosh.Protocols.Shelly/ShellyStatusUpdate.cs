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
    public class ShellyStatusUpdate : ShellyTopicMessage
    {
        public ShellyStatusUpdate() : base()
        {            
        }

        [DataMember(Name = "output")]
        public bool IsOn { get; set; }

        [DataMember(Name = "apower")]
        public double CurrentPower { get; set; }
    }
}
