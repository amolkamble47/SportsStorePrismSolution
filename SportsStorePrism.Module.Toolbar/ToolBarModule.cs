using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Prism.Modularity;
using Microsoft.Practices.Unity;
using Prism.Regions;
using SportsStorePrism.Module.Toolbar.Views;
using SportsStorePrism.Infrastructure;

namespace SportsStorePrism.Module.Toolbar
{
    public class ToolBarModule : IModule
    {
        private IUnityContainer _container;
        private IRegionManager _regionManager;
        public ToolBarModule(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }
        public void Initialize()
        {
            var toolBarView = _container.Resolve<ToolBarView>();
            _regionManager.Regions[RegionNames.ToolBarRegion].Add(toolBarView);
        }
    }
}
