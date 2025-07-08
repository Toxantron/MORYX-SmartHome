using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mosh.Energy
{
    /// <summary>
    /// Facade for the energy manager
    /// </summary>
    public interface IEnergyManager
    {
        /// <summary>
        /// Update current state of the grid
        /// </summary>
        void UpdateGridState(GridState gridState);
    }
}
