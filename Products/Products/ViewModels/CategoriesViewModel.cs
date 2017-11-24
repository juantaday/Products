namespace Products.ViewModels
{
    using Products.Models;
    using Products.Services;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;

    public class CategoriesViewModel :INotifyPropertyChanged
    {
        #region Atributes
        ObservableCollection<Category >_categories;
        #endregion
        #region Services
        ApiService apiService;
        DialogService dialogService;
        #endregion

        #region Properties
        public ObservableCollection<Category> Categories
        {
            get
            {
                return _categories;
            }
            set
            {
                if (_categories != value)
                {
                    _categories = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs("Categories"));
                }
            }
        }
        #endregion
        #region Constructor
        public CategoriesViewModel()
        {
            apiService = new ApiService();
            dialogService = new DialogService();
            Categories = new ObservableCollection<Category>();
            LoadCategories();
        }


        #endregion

        #region Methods
        private async void LoadCategories()
        {
        
            var mainViewModel = MainViewModel.GetInstance();

            if (mainViewModel.Token==null)
            {
                await dialogService.ShowMessage("Error","Do not Ascces Token...");
                return;
            }

            var response = await apiService.GetList<Category>(
                "http://192.168.0.100",
                "/ProductsApi/Api",
               "/Categories",
                mainViewModel.Token.TokenType,
                mainViewModel.Token.AccessToken);

            if (!response.IsSuccess)
            {
                await dialogService.ShowMessage(
                   "Error",
                   response.Message);
                return;
            }

            var categories = (List<Category>)response.Result;
            Categories = new ObservableCollection<Category>(categories.OrderBy(c=>c.Description));
        }
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
