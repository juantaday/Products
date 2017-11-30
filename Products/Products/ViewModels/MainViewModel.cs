namespace Products.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Models;
    using Products.Services;
    using System.Windows.Input;

    public class MainViewModel
    {
        #region Properties
        public LoginViewModel Login { get; set; }
        public CategoriesViewModel Categories { get; set; }
        public ProductsViewModel ProductsView { get; set; }

        public NewCategoryViewModel NewCategory { get; set; }

        public EditCategoryViewModel EditCategory { get; set; }

        public EditAndNewPorductViewModel EditAndNewPorduct { get; set; }

        // 31 7.37
        public TokenResponse Token { get; set; }

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
            var nameCantegory = MainViewModel.GetInstance ();
           nameCantegory.EditAndNewPorduct = new EditAndNewPorductViewModel(new Product(),nameCantegory.ProductsView.CategoryName,Operatio.INSERT);

            await navigationService.Navigate("EditAndNewPorductView");
        }

        private async  void NewCategoryCmd()
        {
            NewCategory = new NewCategoryViewModel();
            
           await navigationService.Navigate("NewCategoryView");
        }

        #endregion

        #region Services
        NavigationService navigationService;
        #endregion
    }
}
