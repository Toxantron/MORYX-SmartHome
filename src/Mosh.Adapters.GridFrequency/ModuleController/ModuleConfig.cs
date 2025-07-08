using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using Moryx.Configuration;
using Moryx.Runtime.Configuration;
using Moryx.Serialization;

namespace Mosh.Adapters.GridFrequency
{
    [DataContract]
    public class ModuleConfig : ConfigBase
    {
        [DataMember]
        public string Host { get; set; }
    }
}