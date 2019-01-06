using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Ploeh.Samples.ProductManagement.Domain
{
    public class Product : INotifyPropertyChanged
    {
        private string name;
        private decimal unitPrice;
        private Guid id;

        public event PropertyChangedEventHandler PropertyChanged = (s, e) => { };

        public Guid Id
        {
            get
            {
                return this.id;
            }
            set
            {
                if (this.id != value)
                {
                    this.id = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                if (this.name != value)
                {
                    this.name = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public decimal UnitPrice
        {
            get
            {
                return this.unitPrice;
            }

            set
            {
                if (this.unitPrice != value)
                {
                    this.unitPrice = value;
                    this.OnPropertyChanged();
                }
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}