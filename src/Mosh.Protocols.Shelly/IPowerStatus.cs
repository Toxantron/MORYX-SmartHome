using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mosh.Protocols.Shelly
{
    public interface IPowerStatus : IShellyMessage
    {
        /// <summary>
        /// Channel on the device
        /// </summary>
        int Channel { get; }

        /// <summary>
        /// Current power
        /// </summary>
        double CurrentPower { get; }
    }
}
