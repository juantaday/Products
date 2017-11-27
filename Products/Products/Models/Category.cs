namespace Products.Models
{
    using GalaSoft.MvvmLight.Command;
    using Products.Services;
    using System;
    using System.Collections.Generic;
    using System.Windows.Input;
    using ViewModels;

    public class Category
    {

        #region Atrubutes
        DialogService dialogService;
        NavigationService navigationService;
        ApiService apiService;
        #endregion

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

        public ICommand  EditCommand {
            get {
                return new RelayCommand(Edit);
            }
        }

        
        public ICommand DeleteCommand
        {
            get
            {
                return new RelayCommand(Delete);
            }
        }
        private async void Edit()
        {
            try
            {
                MainViewModel.GetInstance().EditCategory = new EditCategoryViewModel(this);
                if (navigationService ==null )
                {
                    navigationService = new NavigationService ();
                }
                  await  navigationService.Navigate("EditCategoryView");
            }
            catch (Exception ex)
            {
                if (navigationService == null)
                {
                    dialogService = new DialogService();
                }
                await dialogService.ShowMessage("Error", ex.Message);
            }
        }

        private async void Delete()
        {
            var caregoryView = CategoriesViewModel.GetInstance();
            try
            {
                if (dialogService == null)
                {
                    dialogService = new DialogService();
                }

                var responseDelete = await dialogService.ShowConfirm("Confirm","Are your sore to delete this record,?");

                if (!responseDelete)
                {
                    return;
                }

                caregoryView.IsRefreshing = true;
                var mainviewmodel = MainViewModel.GetInstance();

                if (apiService ==null)
                {
                    apiService = new ApiService();
                }
                var response = await apiService.Delete(
                    "http://192.168.0.100",
                    "/productsApi/api",
                    "/Categories",
                    mainviewmodel.Token.TokenType,
                    mainviewmodel.Token.AccessToken,
                    this);
                if (response.IsSuccess)
                {
                    caregoryView.Categories.Remove(this);
                    return;
                }
                else {
                    caregoryView.IsRefreshing = false;
                    await dialogService.ShowMessage("Error", response.Message );
                }
            }
            catch (Exception ex)
            {
                if (navigationService == null)
                {
                    dialogService = new DialogService();
                }
                await dialogService.ShowMessage("Error", ex.Message);
            }
            finally
            {
                caregoryView.IsRefreshing = false;
            }
        }

        private async  void CategoryCommand()
        {
            try
            {
                var mainViewmodel = MainViewModel.GetInstance();
                mainViewmodel.Productsview = new ProductsViewModel(Products);
                mainViewmodel.Productsview.CategoryName = Description;

                if (navigationService == null)
                {
                    navigationService = new NavigationService();
                }
                await navigationService.Navigate("ProductsView");
            }
            catch (Exception ex)
            {
                if (navigationService == null)
                {
                    dialogService = new DialogService();
                }
                await dialogService.ShowMessage ("Error",ex.Message );
            }
    
        }
        #endregion

        #region Methods
        public override int GetHashCode()
        {
            return CategoryId;
        }
        #endregion

    }
}
