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
        #region Properties
        public ObservableCollection<Product> Products
        {
            get
            {
                return _products;
            }
            set
            {
                if (_products != value)
                {
                    _products = value;
                    PropertyChanged?.Invoke(
                         this,
                         new PropertyChangedEventArgs("Products"));
                }
            }
        }

        public string CategoryName { get; set; }

        #endregion


        #region Constructor

        public ProductsViewModel(int categoryId, List<Product> products, string categoryName)
        {
            this.CategoryName = categoryName;


            foreach (var product in products)
            {
                product.CategoryId = categoryId;
            }
          
            if (products != null && products.Count >0)
            {
                Products = new ObservableCollection<Product>(products.OrderBy(p => p.Description));
            }
         }

        #endregion
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

    }
}
