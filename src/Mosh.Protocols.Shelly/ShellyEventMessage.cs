using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mosh.Protocols.Shelly
{
    [DataContract]
    public class ShellyEventMessage
    {
        public string? Prefix { get; set; }

        [DataMember(Name = "src")]
        public string Source { get; set; }

        [DataMember(Name = "dst")]
        public string Destination { get; set; }

        [DataMember(Name = "method")]
        public string Method { get; set; }

        [DataMember(Name = "params")]
        public MethodParameters Parameters { get; set; }
    }
    [DataContract]
    public class MethodParameters
    {
        [DataMember(Name = "events")]
        public EventParameter[] Events { get; set; }
    }

    [DataContract]
    public class EventParameter
    {
        [DataMember(Name = "component")]
        public string Component { get; set; }

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "event")]
        public string Event { get; set; }
    }
}
