namespace Products.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Products.Models;
    using Products.Services;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;

    public class CategoriesViewModel :INotifyPropertyChanged
    {
        #region Atributes
        ObservableCollection<Category >_categories;
        ObservableCollection<Category> categories;
        string _filter;
        bool _IsRefreshing;
        #endregion
        #region Services
        ApiService apiService;
        DialogService dialogService;
        #endregion

        #region Properties
        public string Filter
        {
            get
            {
                return _filter;
            }
            set
            {
                if (_filter != value)
                {
                    _filter = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(Filter)));
                     if(_filter==null || string.IsNullOrEmpty (_filter))
                    {
                        Categories = categories;
                    }
                }
               
            }
        }

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
            IsRefreshing = false;
         }

        #endregion

        #region Commands
        public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand(Search);
            }
        }

        void Search()
        {
            IsRefreshing = true;
            Categories = new ObservableCollection<Category>(categories
                .Where(c => c.Description.ToLower().Contains(Filter.ToLower()))
                .OrderBy(c => c.Description));
            IsRefreshing = false;
        }

        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand( Refresh);
            }
        }

        public async void  Refresh()
        {
            try
            {
                LoadCategories();
            }
            catch (System.Exception ex)
            { 
              await  dialogService.ShowMessage("Error",ex.Message );
            }
          
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
            try
            {
                Categories.Clear();
                var mainViewModel = MainViewModel.GetInstance();

                if (mainViewModel.Token == null)
                {
                    await dialogService.ShowMessage("Error", "Do not Ascces Token...");
                    return  ;
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
                    return  ;
                }

                var categoriesResult = (List<Category>)response.Result;
                this.categories = new ObservableCollection<Category>(categoriesResult.OrderBy(c => c.Description));
                Categories = this.categories;
                IsRefreshing = false;
                return ;
            }
            catch (System.Exception ex)
            {

                if (dialogService==null )
                {
                    dialogService = new DialogService();
                };
                await  dialogService.ShowMessage("Error",ex.Message);
                return ;
            }
            
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
