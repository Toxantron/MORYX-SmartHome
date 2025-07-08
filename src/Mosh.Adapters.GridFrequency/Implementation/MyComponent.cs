using Moryx.Container;
using Moryx.Threading;
using Mosh.Energy;
using System;
using System.Net.Http;

namespace Mosh.Adapters.GridFrequency
{
    [Component(LifeCycle.Singleton, typeof(IMyComponent))]
    public class Component : IMyComponent
    {
        private HttpClient _httpClient;

        public ModuleConfig Config { get; set; }

        public IEnergyManager EnergyManager { get; set; }

        public IParallelOperations ParallelOperations { get; set; }

        public void Start()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(Config.Host);

            ParallelOperations.ScheduleExecution(FetchFrequency, 100, 100);
        }

        public void Stop()
        {
        }

        private void FetchFrequency()
        {
            var 
        }
    }
}