using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Prism.Modularity;
using Microsoft.Practices.Unity;
using SportsStorePrism.Infrastructure.Abstract;

namespace SportsStorePrism.Module.Services
{
    public class ServicesModule : IModule
    {
        private IUnityContainer _container;
        public ServicesModule(IUnityContainer container)
        {
            _container = container;
        }

        public void Initialize()
        {
            //https://msdn.microsoft.com/en-us/library/ff660872(v=pandp.20).aspx For info in LifeTime
            //For a Singleton 
            _container.RegisterType<IProductRepository, EfProductRepository>(new ContainerControlledLifetimeManager());

            //_container.RegisterType<IProductRepository, EfProductRepository>(new ExternallyControlledLifetimeManager());
            //_container.RegisterType<IProductRepository, EfProductRepository>(new PerThreadLifetimeManager());
            //_container.RegisterType<IProductRepository, EfProductRepository>();
        }
    }
}
