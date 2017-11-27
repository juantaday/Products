namespace Products.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Products.Models;
    using Products.Services;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows.Input;

    public class CategoriesViewModel :INotifyPropertyChanged
    {
        #region Atributes
        ObservableCollection<Category >_categories;
        bool _IsRefreshing;
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
        public bool IsRefreshing
        {
            get
            {
                return _IsRefreshing;
            }
            set
            {
                if (value != _IsRefreshing)
                {
                    _IsRefreshing = value;
                    this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsRefreshing)));
                }
            }
        }
        #endregion

        #region Constructor
        public CategoriesViewModel()
        {
            IsRefreshing = true;
            instance = this;
            apiService = new ApiService();
            dialogService = new DialogService();
            Categories = new ObservableCollection<Category>();
         }

        #endregion

        #region Commands
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(Refresh);
            }
        }

        public  void Refresh()
        {
            LoadCategories();
        }
        #endregion

        #region Singleton
        private static CategoriesViewModel instance;

        public static CategoriesViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new CategoriesViewModel();
            }

            return instance;
        }
        #endregion


        #region Methods
        private async void LoadCategories()
        {
            IsRefreshing = true;

            Categories.Clear();
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
            IsRefreshing = false;
        }

        public void  AddCategory(Category category)
        {
            IsRefreshing = true;
            _categories.Add(category);
            Categories.OrderBy(c => c.Description);
            IsRefreshing = false;
        }

        public void UpdateCategory(Category category)
        {
            IsRefreshing = true;
            var oldCategory = Categories.Where(c => c.CategoryId == category.CategoryId).FirstOrDefault();

            oldCategory.Description = category.Description;

            Categories.OrderBy(c => c.Description);
            IsRefreshing = false;
        }
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion


    }
}
