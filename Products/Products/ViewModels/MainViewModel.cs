namespace Products.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Models;
    using Products.Services;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using System;

    public class MainViewModel
    {
        #region Atributes
        public int CategoryId { get; set; }
        #endregion

        #region Properties
        public LoginViewModel Login { get; set; }
        public CategoriesViewModel Categories { get; set; }
        public ProductsViewModel ProductsView { get; set; }

        public NewCategoryViewModel NewCategory { get; set; }

        public EditCategoryViewModel EditCategory { get; set; }

        public EditAndNewPorductViewModel EditAndNewPorduct { get; set; }

        public NewCustomerViewModel NewCustomer { get; set; }

        public TokenResponse Token { get; set; }

        public ObservableCollection<Menu> MyMenu { get; set; }

        public UbicationsViewModel Ubications { get; set; }

        #endregion

        #region constructor
        public MainViewModel()
        {
            instance = this;

            if (Login == null)
            {
                Login = new LoginViewModel();
            }

            navigationService = new NavigationService();
            LoadMenu();
        }


        #endregion

        #region Methods
        private void LoadMenu()
        {
            MyMenu = new ObservableCollection<Menu>();
            MyMenu.Add(new Menu
            {
                Icon = "ic_settings",
                PageName = "MyProfileView",
                Title = "My Profile",
            });

            MyMenu.Add(new Menu
            {
                Icon = "ic_map",
                PageName = "UbicationsView",
                Title = "Ubications",
            });

            MyMenu.Add(new Menu
            {
                Icon = "ic_exit_to_app",
                PageName = "LoginView",
                Title = "Close sesion",
            });

        }
        #endregion

        #region Singleton
        private static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new MainViewModel();
            }

            return instance;
        }
        #endregion

        #region Commands

        public ICommand NewCategoryCommand
        {
            get
            {
                return new RelayCommand(NewCategoryCmd);
            }
        }


        public ICommand NewProductCommand
        {
            get { 
                return new RelayCommand(NewProductCmd);
            }
        }


        private async void NewProductCmd()
        {
            var nameCantegory = MainViewModel.GetInstance();
            var product = new Product();
            product.CategoryId = CategoryId;


           nameCantegory.EditAndNewPorduct = new EditAndNewPorductViewModel(
               product, 
               nameCantegory.ProductsView.CategoryName,
               Operatio.INSERT);

            await navigationService.NavigateOnMaster("EditAndNewPorductView");
        }

        private async  void NewCategoryCmd()
        {
            NewCategory = new NewCategoryViewModel();
            
           await navigationService.NavigateOnMaster("NewCategoryView");
        }

        #endregion

        #region Services
        NavigationService navigationService;
        #endregion
    }
}
