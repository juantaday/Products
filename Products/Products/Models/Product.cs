namespace Products.Models
{
    using GalaSoft.MvvmLight.Command;
    using Products.Services;
    using Products.ViewModels;
    using System;
    using System.Windows.Input;

    public class Product
    {

        #region Atributes

        #endregion
        #region Properties

        public int CategoryId { get; set; }
        public int ProductId { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public DateTime LastPorcharse { get; set; }
        public string Image { get; set; }
        public double Stock { get; set; }
        public string Reamarks { get; set; }

        public string ImageFullPath
        {
            get
            {
                if (this.Image != null)
                {
                    return string.Format("http://soccerapi.somee.com/{0}",
                    this.Image.Substring(1));
                }
                else
                {
                    return null;
                }

            }
        }

        #endregion

        public ICommand SelectProductCommand
        {
            get
            {
                return new RelayCommand(SelectProduct);
            }
        }

        private async void SelectProduct()
        {
            var nameCantegory = MainViewModel.GetInstance();
            nameCantegory.EditAndNewPorduct = new EditAndNewPorductViewModel(this,nameCantegory.ProductsView.CategoryName, Operatio.UPDATE);

            if (navigationService==null)
            {
                navigationService = new NavigationService();
            }
            await navigationService.Navigate("EditAndNewPorductView");
        }

        #region Services
        NavigationService navigationService;
        #endregion
    }
}
