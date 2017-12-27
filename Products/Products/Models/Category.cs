namespace Products.Models
{
    using GalaSoft.MvvmLight.Command;
    using Products.Services;
    using SQLite;
    using SQLite.Net.Attributes;
    using SQLiteNetExtensions.Attributes;
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

        [PrimaryKey]
        public int CategoryId { get; set; }
        public string Description { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Product> Products { get; set; }
        #endregion

        #region Command
        public ICommand SelectCategoryCommand {
            get
            {
                return new RelayCommand(SelectCategory);
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
                  await  navigationService.NavigateOnMaster("EditCategoryView");
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

        private async  void SelectCategory()
        {
            try
            {
                var mainViewmodel = MainViewModel.GetInstance();
                      
                mainViewmodel.ProductsView = new ProductsViewModel(CategoryId,Products, Description);
                mainViewmodel.CategoryId = CategoryId;
                 if (navigationService == null)
                {
                    navigationService = new NavigationService();
                }
                await navigationService.NavigateOnMaster("ProductsView");
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
