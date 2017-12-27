namespace Products
{
    using Xamarin.Forms;
    using Views;
    using System;
    using Services;
    using Products.Models;
    using Products.ViewModels;

    public partial class App : Application
    {
        #region Services
        ApiService apiService;
        DialogService dialogService;
        DataService dataService;
        NavigationService navigationService;
        #endregion
        #region Properties
        public static NavigationPage Navigator { get; internal set; }
        public static MasterView Master { get; internal set; }
        #endregion

        #region Constructor
        public App()
        {
            InitializeComponent();

            dialogService = new DialogService();
            apiService = new ApiService();
            dataService = new DataService();
            navigationService = new NavigationService();

            var token = dataService.First<TokenResponse>(false);
            if (token != null &&
                token.IsRemembered &&
                token.Expires > DateTime.Now)
            {
                var mainViewModel = MainViewModel.GetInstance();
                mainViewModel.Token = token;
                mainViewModel.Categories = new CategoriesViewModel();
                mainViewModel.Categories.Refresh();
                mainViewModel.Categories.IsRefreshing = false;
                navigationService.SetMainPage("MasterView");
            }
            else
            {
                navigationService.SetMainPage("LoginView");
            }
      
        }

        #endregion
        #region Overrides

        protected override void OnStart()
        {
            //TODO 24 ;22
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
        #endregion

        #region Methods

        public static Action LoginFacebookFail
        {
            get
            {
                return new Action(() => Current.MainPage =
                                  new NavigationPage(new LoginView()));
            }
        }

        public async static void LoginFacebookSuccess(FacebookResponse profile)
        {

            if (profile == null)
            {
                Current.MainPage = new NavigationPage(new LoginView());
                return;
            }

            var apiService = new ApiService();
            var dialogService = new DialogService();

            var token = await apiService.LoginFacebook(
                 "http://192.168.0.100",
                "/ProductsApi/api",
                "/Customers/LoginFacebook",
                profile);

            if (token == null)
            {
                await dialogService.ShowMessage(
                    "Error",
                    "Problem ocurred retrieving user information, try latter.");
                Current.MainPage = new NavigationPage(new LoginView());
                return;
            }

            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Token = token;
            mainViewModel.Categories = new CategoriesViewModel();
            mainViewModel.Categories.Refresh();
            mainViewModel.Categories.IsRefreshing = false;
            Current.MainPage = new MasterView();
        }

        #endregion
    }
}
