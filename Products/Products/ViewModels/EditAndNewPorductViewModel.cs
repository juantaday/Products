using GalaSoft.MvvmLight.Command;
using Products.Models;
using Products.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Products.ViewModels
{
    public class EditAndNewPorductViewModel:Product,INotifyPropertyChanged
    {
        #region Atributes
        Operatio  Operation;

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Properties
        public string CategoryName { get; set; }

        public string TitleOperation { get; set; }

        public string TextButton { get; set; }

        #endregion

        #region Constuctor
        public EditAndNewPorductViewModel(Product product, string CategoryName, Operatio opration)
        {

            CategoryId = product.CategoryId;
            ProductId = product.ProductId;
            Description = product.Description;
            Price = product.Price;
            IsActive = product.IsActive;
            LastPorcharse = product.LastPorcharse;
            Image = product.Image;
            Stock = product.Stock;
            Reamarks = product.Reamarks;

            //initialize Services
            apiService = new ApiService();
            dialogService = new DialogService();

            this.Operation = opration;
            this.CategoryName = string.Format("Category:{0}", CategoryName);
            switch (Operation)
            {
                case Operatio.INSERT:
                    this.TitleOperation = string.Format("New Product");
                    TextButton = string.Format("Save");
                    break;
                case Operatio.UPDATE:
                    this.TitleOperation = string.Format("Edit Product");
                    TextButton = string.Format("Update");
                    break;
                default:
                    this.TitleOperation = string.Format("No Identify");
                    break;
            }

        }

        #endregion

        #region Commands
        public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand(SaveAndUpdate);
            }
        }

        private async  void SaveAndUpdate()
        {
            var mainViewModel = MainViewModel.GetInstance();

            if (this.Operation ==Operatio.UPDATE)
            {

                var response = await apiService.Post<Product>(
               "http://192.168.0.100",
               "/ProductsApi/Api",
              "/Products",
               mainViewModel.Token.TokenType,
               mainViewModel.Token.AccessToken,
               this);

                if (!response.IsSuccess)
                {
                    await dialogService.ShowMessage(
                       "Error",
                       response.Message);
                    return;
                }
            }
            else if (this.Operation == Operatio.INSERT)
            {

                var response = await apiService.Put<Product>(
               "http://192.168.0.100",
               "/ProductsApi/Api",
              "/Products",
               mainViewModel.Token.TokenType,
               mainViewModel.Token.AccessToken,
               this);

                if (!response.IsSuccess)
                {
                    await dialogService.ShowMessage(
                       "Error",
                       response.Message);
                    return;
                }
            }
        }
        #endregion

        #region Services
        ApiService apiService;
        DialogService dialogService;
        #endregion
    }

   

    public enum Operatio {
        INSERT=0,
        UPDATE=1
    }
}
