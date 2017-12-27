namespace Products.Views
{
    using Services;
    using System.Threading.Tasks;
    using ViewModels;
    using Xamarin.Forms;
    using Xamarin.Forms.Maps;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UbicationsView : ContentPage
    {
        #region Atributes
        GeolocatorService geolocatorService;
        #endregion
        public UbicationsView()
        {
            InitializeComponent();
            geolocatorService = new GeolocatorService();
            MoveMapTocurrentPositions();
        }

        #region Methods
        async void MoveMapTocurrentPositions()
        {
            await geolocatorService.GetLocation();
            if (geolocatorService.Latitude != 0 &&
                geolocatorService.Longitude != 0)
            {
                var position = new Position(
                    geolocatorService.Latitude,
                    geolocatorService.Longitude);
                MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(
                position,
                Distance.FromKilometers(.2)));
            }

            await LoadPins();
        }
        async Task LoadPins()
        {
            var ubicationsViewModel = UbicationsViewModel.GetInstance();
            await ubicationsViewModel.LoadPin();
            if (ubicationsViewModel.Pin == null)
            {
                return;
            }
            foreach (var pin in ubicationsViewModel.Pin)
            {
                MyMap.Pins.Add(pin);
            }
        }

        #endregion
    }
}