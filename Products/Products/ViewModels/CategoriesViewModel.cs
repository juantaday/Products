namespace Products.ViewModels
{
    using Products.Models;
    using Products.Services;
    using System;
    using System.Collections.ObjectModel;

    public class CategoriesViewModel
    {
        #region Atributes
        ApiService apiService;
        #endregion
        #region Properties
        public ObservableCollection<Category> Categories { get; set; }

        #endregion
        #region Constructor
        public CategoriesViewModel()
        {
            apiService = new ApiService();
            Categories = new ObservableCollection<Category>();
            LoadCategories();
        }


        #endregion

        #region Methods
        private void LoadCategories()
        {
           
            Categories.Clear();


        } 
        #endregion
    }
}
