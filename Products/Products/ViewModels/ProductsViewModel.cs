using Products.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.ViewModels
{
    public class ProductsViewModel: INotifyPropertyChanged
    {
        #region Atributes
        ObservableCollection<Product> _products;
        #endregion
        public ObservableCollection<Product> Products
        {
            get
            {
                return _products;
            }
            set
            {
                if (_products!=value)
                {
                    _products = value;
                    PropertyChanged?.Invoke(
                         this,
                         new PropertyChangedEventArgs("Products"));
                }
            }
        }

        public ProductsViewModel(List<Product> products)
        {
           Products = new ObservableCollection<Product>(products.OrderBy(p=>p.Description ));
        }
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

    }
}
