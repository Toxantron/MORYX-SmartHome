using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mosh.Energy
{
    public class GridState
    {
        public double Frequency { get; set; }

        public double Price { get; set; }
        
        public double PrimaryControlPower { get; set; }
    }
}
