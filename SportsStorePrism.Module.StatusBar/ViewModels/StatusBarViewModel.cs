using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Prism.Mvvm;
using Prism.Events;
using SportsStorePrism.Infrastructure;

namespace SportsStorePrism.Module.StatusBar.ViewModels
{
    public class StatusBarViewModel: BindableBase
    {
        private IEventAggregator _eventAggregator;
        private string _statusBarContent;
        public StatusBarViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            StatusBarContent = "Message from StatusBar";
            _eventAggregator.GetEvent<ProductsMessagingEvent>().Subscribe(ProductsMessaging);
        }

        private void ProductsMessaging(string message)
        {
            StatusBarContent = message;
        }

        public string StatusBarContent { get => _statusBarContent; set => SetProperty(ref _statusBarContent, value); }
    }
}
