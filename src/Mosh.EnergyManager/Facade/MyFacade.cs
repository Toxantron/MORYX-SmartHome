using System;
using Moryx.Runtime.Modules;

namespace Mosh.EnergyManager
{
    public class MyFacade : IFacadeControl, IMyFacade
    {
        public IMyComponent Component { get; set; }

        public void Activate()
        {
        }

        public void Deactivate()
        {
        }

        public Action ValidateHealthState { get; set; }

        public int Value => 42;
    }
}