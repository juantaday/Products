namespace Products.ViewModels
{
    using Models;
    using Services;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;
    using Xamarin.Forms.Maps;

    public class UbicationsViewModel
    {
        #region Services
        DialogService dialogService;
        GeolocatorService geolocatorService;
        ApiService apiService;
        #endregion

        #region Constructor
        public UbicationsViewModel()
        {
            instance = this;
            apiService = new ApiService();
            dialogService = new DialogService();
            geolocatorService = new GeolocatorService();
        }
        #endregion

        #region Properties
        public ObservableCollection<Pin> Pin { get; set; }
        #endregion

        #region Singleton
        private static UbicationsViewModel instance;

        public static UbicationsViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new UbicationsViewModel();
            }

            return instance;
        }
        #endregion

        #region Methods
        public async Task LoadPin()
        {
            var mainViewModel = MainViewModel.GetInstance();

            if (mainViewModel.Token == null)
            {
                await dialogService.ShowMessage("Error", "Do not Ascces Token...");
                return;
            }

            var response = await apiService.GetList<Ubication>(
                "http://192.168.0.100",
                "/ProductsApi/Api",
               "/Ubications",
                mainViewModel.Token.TokenType,
                mainViewModel.Token.AccessToken);

            if (!response.IsSuccess)
            {
                await dialogService.ShowMessage(
                   "Error",
                   response.Message);
                return;
            }
            var ubications = (List<Ubication>)response.Result;
            Pin = new ObservableCollection<Pin>();

            foreach (var ubication in ubications)
            {
                Pin.Add(new Pin
                {
                    Address = ubication.Address,
                    Label= ubication.Description,
                    Position  = new Position (ubication.Latitude,ubication.Longitude),
                    Type= PinType.Place
                });
            }
   
            return;
        }
        #endregion
    }
}
