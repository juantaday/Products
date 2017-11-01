﻿namespace Products.ViewModels
{
    using Models;
    public class MainViewModel
    {
        #region Properties
        public LoginViewModel Login { get; set; }
        public CategoriesViewModel Categories { get; set; }

        public TokenResponse  Token { get; set; }

        #endregion

        #region constructor
        public MainViewModel()
        {
            Login = new LoginViewModel();
        }
        #endregion

        #region Singleton
        static MainViewModel instance;
        public static  MainViewModel GetInstance()
            {
                if (instance == null)
                {
                    return new MainViewModel();
                }
                else
                {
                    return instance;
                }
            }

        #endregion
    }
}
