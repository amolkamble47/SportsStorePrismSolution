using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.ObjectModel;

using Prism.Mvvm;
using SportsStorePrism.Infrastructure.Abstract;
using SportsStorePrism.Infrastructure.Entities;

namespace SportsStorePrism.Module.SSContent.ViewModels
{
    public class ProductViewModel: BindableBase
    {
        private IProductRepository _productRepository;
        private ObservableCollection<Product> _products;
        public ProductViewModel(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            GetProducts();
        }

        public ObservableCollection<Product> Products { get => _products; set => SetProperty(ref _products, value); }

        async Task GetProducts(string category = null)
        {
            Products = new ObservableCollection<Product>(await _productRepository.GetProductsAsync());
        }
    }
}
