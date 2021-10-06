using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using Ploeh.Samples.ProductManagement.Domain;
using Ploeh.Samples.ProductManagement.PresentationLogic.UICommands;

namespace Ploeh.Samples.ProductManagement.PresentationLogic.ViewModels
{
    // ---- Code Listing 7.4 ----
    public class MainViewModel : IViewModel,
        INotifyPropertyChanged
    {
        private readonly INavigationService navigator;
        private readonly IProductRepository productRepository;

        public MainViewModel(
            INavigationService navigator,
            IProductRepository productRepository)
        {
            this.navigator = navigator;
            this.productRepository = productRepository;

            this.AddProductCommand =
                new RelayCommand(this.AddProduct);
            this.EditProductCommand =
                new RelayCommand(this.EditProduct);
        }

        public IEnumerable<Product> Model { get; set; }
        public ICommand AddProductCommand { get; }
        public ICommand EditProductCommand { get; }

        public event PropertyChangedEventHandler
            PropertyChanged = (s, e) => { };

        public void Initialize(
            Action whenDone, object model)
        {
            this.Model = this.productRepository.GetAll();
            this.PropertyChanged.Invoke(this,
                new PropertyChangedEventArgs("Model"));
        }

        private void AddProduct()
        {
            this.navigator.NavigateTo<NewProductViewModel>(
                whenDone: this.GoBack);
        }

        private void EditProduct(object product)
        {
            this.navigator.NavigateTo<EditProductViewModel>(
                whenDone: this.GoBack,
                model: product);
        }

        private void GoBack()
        {
            this.navigator.NavigateTo<MainViewModel>();
        }
    }
}