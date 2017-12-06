

namespace Products.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Plugin.Media;
    using Plugin.Media.Abstractions;
    using Products.Models;
    using Products.Services;
    using System.ComponentModel;
    using System.Windows.Input;
    using Xamarin.Forms;
    using Helpers;
    public class EditAndNewPorductViewModel:Product,INotifyPropertyChanged
    {
        #region Atributes
        Operatio  Operation;
        bool _isRunning;
        bool _isEnabled;
        ImageSource _imageSource;
        MediaFile _file;
        #endregion

        #region Properties
        public string CategoryName { get; set; }

        public string TitleOperation { get; set; }

        public string TextButton { get; set; }

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

        public ImageSource ImageSource
        {
            get
            {
                return _imageSource;
            }

            set {
                if (value!=_imageSource)
                {
                    _imageSource = value;
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(ImageSource)));
                }
            }

        }

        #endregion

        #region Constuctor
        public EditAndNewPorductViewModel(Product product, string CategoryName, Operatio opration)
        {
            if (product.ImageFullPath != null )
            {
                ImageSource = product.ImageFullPath;
            }
            else
            {
                ImageSource = "ic_do_not_disturb_alt";
            }
            CategoryId = product.CategoryId;
            ProductId = product.ProductId;
            Description = product.Description;
            Price = product.Price;
            IsActive = product.IsActive;
            LastPorcharse = product.LastPorcharse;
          
            Stock = product.Stock;
            Reamarks = product.Reamarks;

            //initialize Services
            apiService = new ApiService();
            dialogService = new DialogService();
            navigationService = new NavigationService();

            this.Operation = opration;
            this.CategoryName = string.Format("Category:{0}", CategoryName);
            IsEnabled = true;

            switch (Operation)
            {
                case Operatio.INSERT:
                    this.TitleOperation = string.Format("New Product");
                    TextButton = string.Format("Save");
                    break;
                case Operatio.UPDATE:
                    this.TitleOperation = string.Format("Edit Product");
                    TextButton = string.Format("Update");
                    break;
                default:
                    this.TitleOperation = string.Format("No Identify");
                    break;
            }

        }

        #endregion

        #region Commands

        public ICommand ChangeImageCommand {
            get
            {
                return new RelayCommand(ChangeImage);
            }
        }
        private async  void ChangeImage()
        {
            await CrossMedia.Current.Initialize();
            if (CrossMedia.Current.IsCameraAvailable &&
                CrossMedia.Current.IsTakePhotoSupported)
            {
                var source = await  dialogService.ShowImageOptios();
                if (source == null)
                {
                    _file = null;
                    return;
                }
                else if (source == "From camera")
                {
                    var storeCameraMediaOptionsf = new  StoreCameraMediaOptions();
                    storeCameraMediaOptionsf.Directory = "Sample";
                    storeCameraMediaOptionsf.Name = "test.jpg";
                    storeCameraMediaOptionsf.PhotoSize  = PhotoSize.Small;

                    _file = await CrossMedia.Current.TakePhotoAsync(storeCameraMediaOptionsf);
                       
                }
                else if (source == "From gallery")
                {

                    _file = await CrossMedia.Current.PickPhotoAsync();
                }
                if (_file!=null )
                {
                    ImageSource = ImageSource.FromStream(()=>
                        {
                            var stream = _file.GetStream();
                            return stream;
                        }
                    );
                }

            }
        }

        public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand(SaveAndUpdate);
            }
        }

        private async  void SaveAndUpdate()
        {
            byte [] imageArray  = null ;

            try
            {

                if (_file != null)
                {
                    imageArray = FilesHelper.ReadFully(_file.GetStream());
                    _file.Dispose();
                }
                this.ImageArray = imageArray;
                var mainViewModel = MainViewModel.GetInstance();

                var newProduct = new Product
                {
                    CategoryId = this.CategoryId,
                    Description = Description,
                    ImageArray = ImageArray,
                    IsActive = IsActive,
                    LastPorcharse = LastPorcharse,
                    Price = Price,
                    ProductId = this.ProductId,
                    Reamarks = this.Reamarks,
                    Stock = Stock
                };

                if (this.Operation == Operatio.UPDATE)
                {
                    IsRunning = true;
                    IsEnabled = false;
                    var response = await apiService.Put<Product>(
                   "http://192.168.0.100",
                   "/ProductsApi/Api",
                  "/Products",
                   mainViewModel.Token.TokenType,
                   mainViewModel.Token.AccessToken,
                   newProduct);

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
                    await navigationService.Back();
                }
                else if (this.Operation == Operatio.INSERT)
                {
                    IsEnabled = false;
                    IsRunning = true;
                    var response = await apiService.Post<Product>(
                   "http://192.168.0.100",
                   "/ProductsApi/Api",
                  "/Products",
                   mainViewModel.Token.TokenType,
                   mainViewModel.Token.AccessToken,
                   newProduct);

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
                    await navigationService.Back();
                }
            }
            catch (System.Exception ex)
            {

              await   dialogService.ShowMessage("Error",ex.Message );
                return;
            }

        }
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Services
        ApiService apiService;
        DialogService dialogService;
        NavigationService navigationService;
        #endregion
    }

   

    public enum Operatio {
        INSERT=0,
        UPDATE=1
    }
}
