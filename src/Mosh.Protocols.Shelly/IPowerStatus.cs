using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mosh.Protocols.Shelly
{
    public interface IPowerStatus
    {
        /// <summary>
        /// Current power
        /// </summary>
        double CurrentPower { get; set; }
    }
}
