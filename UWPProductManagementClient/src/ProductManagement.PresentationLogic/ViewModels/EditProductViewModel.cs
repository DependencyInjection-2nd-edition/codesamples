using System;
using System.ComponentModel;
using System.Windows.Input;
using Ploeh.Samples.ProductManagement.Domain;
using Ploeh.Samples.ProductManagement.PresentationLogic.UICommands;

namespace Ploeh.Samples.ProductManagement.PresentationLogic.ViewModels
{
    public class EditProductViewModel : IViewModel, INotifyPropertyChanged
    {
        private readonly IProductRepository productRepository;

        private Action whenDone;

        public EditProductViewModel(IProductRepository productRepository)
        {
            if (productRepository == null) throw new ArgumentNullException("productRepository");

            this.productRepository = productRepository;

            this.SaveProductCommand = new RelayCommand(this.SaveProduct);
            this.DeleteProductCommand = new RelayCommand(this.DeleteProduct);
            this.CancelCommand = new RelayCommand(this.Cancel);
        }

        public event PropertyChangedEventHandler PropertyChanged = (s, e) => { };

        public Product Model { get; private set; }

        public ICommand SaveProductCommand { get; }
        public ICommand DeleteProductCommand { get; }
        public ICommand CancelCommand { get; }

        public void Initialize(Action whenDone, object model)
        {
            if (!(model is Product)) throw new ArgumentException("model should be a Product.");
            if (whenDone == null) throw new ArgumentNullException("whenDone");

            Guid productId = ((Product)model).Id;

            this.whenDone = whenDone;
            this.Model = this.productRepository.GetById(productId);

            this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Model"));
        }

        private void SaveProduct()
        {
            this.productRepository.Update(this.Model);
            this.whenDone();
        }

        private void DeleteProduct()
        {
            this.productRepository.Delete(this.Model.Id);
            this.whenDone();
        }

        private void Cancel()
        {
            this.whenDone();
        }
    }
}