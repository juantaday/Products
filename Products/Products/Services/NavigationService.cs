namespace Products.Services
{
    using System;
    using System.Threading.Tasks;
    using Views;
    using Xamarin.Forms;

    public class NavigationService
    {
        public void SetMainPage(string pageName)
        {
            switch (pageName)
            {
                case "LoginView":
                    Application.Current.MainPage = new NavigationPage(new LoginView());
                    break;
                case "MasterView":
                    Application.Current.MainPage = new MasterView();
                    break;
            }
        }


        public async Task NavigateOnMaster(string pageName)
        {
            App.Master.IsPresented = false;
            switch (pageName)
            {
                case "CategoriesView":
                await Application.Current.MainPage.Navigation.PushAsync(
                    new CategoriesView());
                    break;
                case "ProductsView":
                    await App.Navigator.Navigation.PushAsync(
                        new ProductsView());
                    break;
                case "NewCategoryView":
                    await App.Navigator.Navigation.PushAsync(
                      new NewCategoryView());
                    break;
                case "EditCategoryView":
                    await App.Navigator.Navigation.PushAsync(
                      new EditCategoryView());
                    break;
                case "EditAndNewPorductView":
                    await App.Navigator.Navigation.PushAsync(
                        new EditAndNewPorductView());
                    break;
                case "UbicationsView":
                    await App.Navigator.Navigation.PushAsync(
                        new UbicationsView());
                    break;
            }
           
        }

        public async Task NavigateOnLogin(string pageName)
        {
            switch (pageName)
            {
                case "NewCustomerView":
                    await Application.Current.MainPage.Navigation.PushAsync(new NewCustomerView());
                    break;
                case "LoginFacebookView":
                    await Application.Current.MainPage.Navigation.PushAsync(new LoginFacebookView());
                    break;
                default:
                    break;
            }

        }


        public async  Task BackOnMaster()
        {
          await   App.Navigator.Navigation.PopAsync();
        }
        public async Task BackOnLogin()
        {
            await App.Navigator.Navigation.PopAsync();
        }
    }
}
 