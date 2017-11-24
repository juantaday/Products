namespace Products.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using System;
    using System.ComponentModel;
    using System.Windows.Input;
    using Services;
    using Xamarin.Forms;
    using Views;
    public class LoginViewModel : INotifyPropertyChanged
    {
        #region Attributes
        string _email;
        string _password;
        bool _isToggled;
        bool _isRunning;
        bool _isEnabled;
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Properties
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                if (value !=_email )
                {
                    _email = value;
                    this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Email)));
                }
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                if (value != _password)
                {
                    _password = value;
                    this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Password)));
                }
            }
        }

        public bool IsToggled
        {
            get
            {
                return _isToggled;
            }
            set
            {
                if (value !=_isToggled )
                {
                    _isToggled = value;
                    this.PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(nameof(IsToggled)));
                }
            }
        }

        public bool IsRunning
        {
            get
            {
                return _isRunning;
            }
            set
            {
                if (value != _isRunning)
                {
                    _isRunning = value;
                    this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsRunning)));
                }
            }
        }

        public bool IsEnabled
        {
            get
            {
                return _isEnabled;
            }
            set
            {
                if (value != _isEnabled)
                {
                    _isEnabled = value;
                    this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsEnabled)));
                }
            }
        }


        #endregion

        #region Commands
        public ICommand LoginCommand
        {
            get
            {
                return new RelayCommand(Login);
            }

                }

        private async  void Login()
        {
            if (string.IsNullOrEmpty (Email))
            {
                await dialogService.ShowMessage(
                    "Error",
                    "You most enter an email.");
                return;
            }
            if (string.IsNullOrEmpty(Password))
            {
                await dialogService.ShowMessage(
                    "Error",
                    "You most enter a password.");
                return;
            }
            IsRunning = true;
            IsEnabled = false;
            var connection = await apiService.CheckConnection();
            if (!connection.IsSuccess )
            {
                IsRunning = false ;
                IsEnabled = true ;
                await dialogService.ShowMessage("Error", connection.Message);
                return;
            }

            var response = await apiService.GetToken(
                "http://192.168.0.100/ProductsApi/", 
                Email,
                Password);
            if (response==null)
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogService.ShowMessage(
                    "Error", 
                    "The service is not available, please try latter");
                return;
            }
            if  (string.IsNullOrEmpty (response.AccessToken))
            {
                IsRunning = false;
                IsEnabled = true;
                await dialogService.ShowMessage(
                    "Error",
                    response.ErrorDescription );
                return;
            }

            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Token = response;
            mainViewModel.Categories = new CategoriesViewModel();

            navigationService = new NavigationService();
            await  navigationService.Navigate("CategoriesView");
          
            IsRunning = false;
            IsEnabled = true;
        }
        #endregion

        #region Constructor
        public LoginViewModel()
        {
            IsEnabled = true;
            IsToggled = true;

            Email = "juantadaymalan3@gmail.com";
            Password = "juan123.";

            dialogService = new DialogService();
            apiService = new ApiService();
        }

        #endregion

        #region Services 
        DialogService dialogService;
        ApiService apiService;
        NavigationService navigationService;
        #endregion

    }
}
