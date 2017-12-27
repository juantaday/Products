namespace Products.Models
{
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    using Services;
    using ViewModels;
    public class Menu
    {
        #region Services
        NavigationService navigationService;
        DataService dataService;
        #endregion
        #region Properties
        public string Icon { get; set; }

        public string Title { get; set; }

        public string PageName { get; set; }
        #endregion
        #region Commads
        public ICommand NavigateCommand
        {
            get {
                return new RelayCommand(Navigate);
            }
        }

        async void Navigate()
        {
            if (navigationService ==null )
            {
                navigationService = new NavigationService();
            }
            switch (PageName)
            {
                case "LoginView":
                    var mainViewModel = MainViewModel.GetInstance();
                    mainViewModel.Token.IsRemembered = false;
                    if (dataService==null)
                    {
                        dataService = new DataService();
                    }
                    dataService.DeleteAll<TokenResponse>();
                    dataService.Insert(mainViewModel.Token);
                    navigationService.SetMainPage("LoginView");
                    break;
                case "UbicationsView":
                    MainViewModel.GetInstance().Ubications = new UbicationsViewModel();
                   await  navigationService.NavigateOnMaster("UbicationsView");
                    break;
                default:
                    break;
            }
        }

        #endregion
    }

}
