using System;

using Prism.Mvvm;
using SportsStorePrism.Infrastructure.Abstract;
using SportsStorePrism.Infrastructure.Entities;
using Prism.Commands;
using System.ComponentModel;
using Prism.Regions;
using SportsStorePrism.Infrastructure;
using System.Linq;

namespace SportsStorePrism.Module.Products.ViewModels
{
    public class AddEditProductViewModel : BindableBase, INavigationAware
    {
        private IRegionManager _regionManager;
        private IProductRepository _productRepository;
        private SimpleEditableProduct _product;
        private Product _editableProduct;
        private bool _editFlag;

        public AddEditProductViewModel(IProductRepository productRepository, IRegionManager regionManager)
        {
            _regionManager = regionManager;
            _productRepository = productRepository;
            CancelCommand = new DelegateCommand(OnCancel);
            SaveCommand = new DelegateCommand(OnSave, CanSave);
        }
        public void SetProduct(Product product)
        {
            _editableProduct = product;
            if (Product != null) Product.ErrorsChanged -= RaiseCanExecuteChanged;
            Product = new SimpleEditableProduct();
            Product.ErrorsChanged += RaiseCanExecuteChanged;
            CopyProduct(product, Product);
        }
        private void RaiseCanExecuteChanged(object sender, DataErrorsChangedEventArgs e)
        {
            SaveCommand.RaiseCanExecuteChanged();
        }

        private void UpdateProduct(SimpleEditableProduct source, Product target)
        {
            target.ProductName = source.ProductName;
            target.Description = source.Description;
            target.Price = source.Price;
            target.Category = source.Category;
        }

        private void CopyProduct(Product source, SimpleEditableProduct target)
        {
            target.ProductId = source.ProductId;
            if (EditFlag)
            {
                target.ProductName = source.ProductName;
                target.Description = source.Description;
                target.Price = source.Price;
                target.Category = source.Category;
            }
        }

        public bool EditFlag { get => _editFlag; set => SetProperty(ref _editFlag, value); }

        public SimpleEditableProduct Product { get => _product; set => SetProperty(ref _product, value); }

        public event Action Done = delegate { };
        public DelegateCommand CancelCommand { get; private set; }
        private void OnCancel()
        {
            _regionManager.RequestNavigate(RegionNames.ProductRegion, "ProductView");
            //Done();
        }
        public DelegateCommand SaveCommand { get; private set; }
        private async void OnSave()
        {
            UpdateProduct(Product, _editableProduct);
            if (EditFlag)
            {
                await _productRepository.UpdateProductAsync(_editableProduct);
            }
            else
            {
                await _productRepository.AddProductAsync(_editableProduct);
            }
            var parameters = new NavigationParameters();
            parameters.Add("editFlag", EditFlag.ToString());
            _regionManager.RequestNavigate(RegionNames.ProductRegion, "ProductView", parameters);
            //Done();
        }
        private bool CanSave()
        {
            return Product != null ? !Product.HasErrors : false;
            //return !Product.HasErrors;
            //return true;
        }

        #region INavigationAware Members
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters.Count() != 0)
            {
                var flag = navigationContext.Parameters["editFlag"].ToString();
                EditFlag = flag == "True" ? true : false;
                if (EditFlag)
                {
                    var prod = navigationContext.Parameters["prod"] as Product;
                    if (prod != null)
                    {
                        SetProduct(prod);
                    }
                }
                else
                {
                    SetProduct(new Product());
                } 
            }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
            //throw new NotImplementedException();
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        } 
        #endregion
    }
}
