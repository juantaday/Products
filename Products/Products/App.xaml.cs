﻿namespace Products
{
    using Xamarin.Forms;
    using Views;
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new LoginView());
        }

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
    }
}