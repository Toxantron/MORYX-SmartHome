using Moryx.AbstractionLayer.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mosh.Resources
{
    /// <summary>
    /// API that represents a device
    /// </summary>
    public interface IDevice : IPublicResource
    {
        double ActivePower { get; }

        public void SetState(DeviceState state);
    }

    public enum DeviceState
    {
        On,
        Off,
        Toggle
    }
}
