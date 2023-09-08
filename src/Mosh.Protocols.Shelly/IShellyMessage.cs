using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mosh.Protocols.Shelly
{
    public interface IShellyMessage
    {
        /// <summary>
        /// Configured prefix for the devices
        /// </summary>
        string Prefix { get; set; }
    }
}
