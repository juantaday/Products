namespace Products.ViewModels
{
    using Models;
    public class MainViewModel
    {
        #region Properties
        public LoginViewModel Login { get; set; }
        public CategoriesViewModel Categories { get; set; }
        public ProductsViewModel Productsview { get; set; }
        // 31 7.37
        public TokenResponse  Token { get; set; }

        #endregion

        #region constructor
        public MainViewModel()
        {
            instance = this;

            if (Login ==null)
            {
                Login = new LoginViewModel();
            }

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
    }
}
