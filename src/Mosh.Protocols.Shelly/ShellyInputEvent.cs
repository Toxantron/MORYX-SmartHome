using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mosh.Protocols.Shelly
{
    public class ShellyInputEvent : IShellyMessage
    {
        public string Prefix { get; set; }

        public int Input { get; set; }

        public string InputTopic
        {
            get => $"input:{Input}";
            set => Input = int.Parse(value.Split(':')[1]);
        }

        [DataMember(Name = "state")]
        public bool State { get; set; }
    }
}
