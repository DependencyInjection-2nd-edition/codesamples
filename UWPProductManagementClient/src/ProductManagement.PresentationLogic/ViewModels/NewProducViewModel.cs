using System;
using System.ComponentModel;
using System.Windows.Input;
using Ploeh.Samples.ProductManagement.Domain;
using Ploeh.Samples.ProductManagement.PresentationLogic.UICommands;

namespace Ploeh.Samples.ProductManagement.PresentationLogic.ViewModels
{
    public class NewProductViewModel : IViewModel, INotifyPropertyChanged
    {
        private readonly IProductRepository productRepository;

        private Action whenDone;

        public NewProductViewModel(IProductRepository productRepository)
        {
            if (productRepository == null) throw new ArgumentNullException("productRepository");

            this.productRepository = productRepository;

            this.SaveProductCommand = new RelayCommand(this.SaveProduct);
            this.CancelCommand = new RelayCommand(this.Cancel);
        }

        public event PropertyChangedEventHandler PropertyChanged = (s, e) => { };

        public Product Model { get; private set; }

        public ICommand CancelCommand { get; }
        public ICommand SaveProductCommand { get; }

        public void Initialize(Action whenDone, object model)
        {
            if (whenDone == null) throw new ArgumentNullException("whenDone");

            this.whenDone = whenDone;

            this.Model = new Product { Id = Guid.NewGuid() };

            this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Model"));
        }

        private void Cancel()
        {
            this.whenDone();
        }

        private void SaveProduct()
        {
            this.productRepository.Insert(this.Model);
            this.whenDone();
        }
    }
}