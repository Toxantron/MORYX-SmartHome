using System.ComponentModel;
using Microsoft.Extensions.Logging;
using Moryx.Configuration;
using Moryx.Container;
using Moryx.Runtime.Container;
using Moryx.Runtime.Modules;
using Mosh.Energy;

namespace Mosh.Adapters.GridFrequency
{
    [Description("Description of FrequencyAdapter")]
    public class ModuleController : ServerModuleFacadeControllerBase<ModuleConfig>
    {
        internal const string ModuleName = "FrequencyAdapter";

        public override string Name => ModuleName;

        /// <summary>
        /// Import a facade, e.g.IResourceManagement
        /// </summary>
        [RequiredModuleApi(IsStartDependency = true, IsOptional = false)]
        public IEnergyManager EnergyManager { get; set; }

        /// <summary>
        /// Create a new instance of the module
        /// </summary>
        public ModuleController(IModuleContainerFactory containerFactory, IConfigManager configManager, ILoggerFactory loggerFactory)
            : base(containerFactory, configManager, loggerFactory)
        {
        }

        #region State transition

        /// <summary>
        /// Code executed on start up and after service was stopped and should be started again
        /// </summary>
        protected override void OnInitialize()
        {

            // Register model
            //Container.SetInstance(MyModel);

            // Register required facade
            Container.SetInstance(EnergyManager);
        }

        /// <summary>
        /// Code executed after OnInitialize
        /// </summary>
        protected override void OnStart()
        {
            // Start component
            Container.Resolve<IMyComponent>().Start();

            ActivateFacade(_myFacade);
        }

        /// <summary>
        /// Code executed when service is stopped
        /// </summary>
        protected override void OnStop()
        {
            // Tear down facades
            DeactivateFacade(_myFacade);

            Container.Resolve<IMyComponent>().Stop();
        }

        #endregion

        #region FacadeContainer

        private readonly MyFacade _myFacade = new MyFacade();
        IMyFacade IFacadeContainer<IMyFacade>.Facade => _myFacade;

        #endregion
    }
}