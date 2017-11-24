namespace Products.Models
{
    using ViewModels;
    using GalaSoft.MvvmLight.Command;
    using System.Collections.Generic;
    using System.Windows.Input;
    using System;
    using Products.Views;
    using Xamarin.Forms;
    using Views;

    public class Category
    {
        #region Properties
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public List<Product> Products { get; set; } 
        #endregion
 
        #region Command
        public ICommand SelectCategoryCommand {
            get
            {
                return new RelayCommand(CategoryCommand);
            }
        }

        private async  void CategoryCommand()
        {
            var mainViewmodel = MainViewModel.GetInstance();
            mainViewmodel.Productsview = new ProductsViewModel(Products);
            mainViewmodel.Productsview.CategoryName = Description;
           await Application.Current.MainPage.Navigation.PushAsync(new ProductsView());
        }
        #endregion
    }
}
