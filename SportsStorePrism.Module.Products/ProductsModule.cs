using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Prism.Modularity;
using Prism.Regions;
using Microsoft.Practices.Unity;
using SportsStorePrism.Module.Products.Views;
using SportsStorePrism.Infrastructure;

namespace SportsStorePrism.Module.Products
{
    public class ProductsModule : IModule
    {
        private IUnityContainer _container;
        private IRegionManager _regionManager;
        public ProductsModule(IUnityContainer container, IRegionManager regionManager) 
        {
            _container = container;
            _regionManager = regionManager;
        }
        public void Initialize()
        {
            //Register for Navigation
            _container.RegisterType(typeof(object), typeof(ProductView), "ProductView");
            _container.RegisterType(typeof(object), typeof(AddEditProductView), "AddEditProductView");

            //Navigate to ProductView
            _regionManager.RequestNavigate(RegionNames.ProductRegion, "ProductView");

            //First
            //var productView = _container.Resolve<ProductView>();
            //_regionManager.Regions[RegionNames.ProductRegion].Add(productView);

        }
    }
}
