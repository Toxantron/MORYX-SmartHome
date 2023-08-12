using Moryx.Container;

namespace Mosh.EnergyManager
{
    [Component(LifeCycle.Singleton, typeof(IMyComponent))]
    public class Component : IMyComponent
    {
        public void Start()
        {
        }

        public void Stop()
        {
        }
    }
}