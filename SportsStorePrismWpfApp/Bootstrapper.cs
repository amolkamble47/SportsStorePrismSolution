using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Prism.Modularity;
using SportsStorePrism.Module.Products;
using SportsStorePrism.Module.Services;
using SportsStorePrism.Module.StatusBar;
using SportsStorePrism.Module.Toolbar;

namespace SportsStorePrismWpfApp
{
    public class Bootstrapper: UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.TryResolve<Shell>();

            #region Default
            //return base.CreateShell(); 
            #endregion
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();
            App.Current.MainWindow = Shell as Window;
            App.Current.MainWindow.Show();
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            ModuleCatalog moduleCatalog = new ModuleCatalog();
            moduleCatalog.AddModule(typeof(ServicesModule));
            moduleCatalog.AddModule(typeof(ProductsModule));
            moduleCatalog.AddModule(typeof(StatusBarModule));
            moduleCatalog.AddModule(typeof(ToolBarModule));
            return moduleCatalog;
            //return base.CreateModuleCatalog();
        }
    }
}
