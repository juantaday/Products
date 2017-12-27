namespace Products.ViewModels
{
    using System;
    using GalaSoft.MvvmLight.Command;
    using Services;
    using System.ComponentModel;
    using System.Windows.Input;
    using Models;
    public class NewCategoryViewModel : INotifyPropertyChanged
    {
        #region Attributes
        bool _isRunning;
        bool _isEnabled;
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Properties

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

        public string  Description { get; set; }

        #endregion

        #region Commands
        public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand(Save);
            }

        }

        private async void Save()
        {
            try
            {
                if (string.IsNullOrEmpty(Description))
                {
                    await dialogService.ShowMessage(
                        "Error",
                        "You most enter an description.");
                    return;
                }

                IsRunning = true;
                IsEnabled = false;

                var category = new Category();

                category.Description = Description;

                var mainviewmodel = MainViewModel.GetInstance();

                var response = await apiService.Post(
                    "http://192.168.0.100",
                    "/productsApi/api",
                    "/Categories",
                    mainviewmodel.Token.TokenType,
                    mainviewmodel.Token.AccessToken,
                    category);
                if (!response.IsSuccess)
                {
                    IsRunning = false;
                    IsEnabled = true;
                    await dialogService.ShowMessage(
                        "Error",
                        response.Message);
                    return;
                }

                IsRunning = false;
                IsEnabled = true;

                category = (Category)response.Result;

                var categoriesViewModel = CategoriesViewModel.GetInstance();
                categoriesViewModel.AddCategory(category);
                await navigationService.BackOnMaster();

            }
            catch (Exception ex)
            {
               await  dialogService.ShowMessage ("Error",ex.Message );
            }
        }
        #endregion

        #region Constructor
        public NewCategoryViewModel()
        {
            IsEnabled = true;

            navigationService = new NavigationService();
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
