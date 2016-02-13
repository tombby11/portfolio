using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Maps.MapControl.WPF;
using WeatherApp.PresentationContext.Models;
using WeatherApp.PresentationContext.ViewModels;
using Location = Microsoft.Maps.MapControl.WPF.Location;

namespace WeatherApp.WPF.Views
{
    public partial class MainWindow : Window
    {
        private readonly CitiesViewModel _viewModel;
        private List<Pushpin> _pushpins; 
        public MainWindow()
        {
            _viewModel = new CitiesViewModel();
            Title = "Code Challenge Weather App";
            InitializeComponent();
            forecastListView.citySearchBoxView.searchBox.SuggestionSelected += SearchBox_SuggestionSelected;
            _pushpins = new List<Pushpin>();
            InitializeMapPinsAndCenter();
        }

     

        private void InitializeMapPinsAndCenter()
        {
            foreach (var cityModel in _viewModel.Cities)
            {
                var pushpin = new Pushpin();
                pushpin.Name = cityModel.CityName;
                pushpin.MouseDown += Pushpin_OnMouseDown;
                pushpin.Location = new Location(cityModel.Latitude, cityModel.Longitude);
                mapView._map.Children.Add(pushpin);
            }
            mapView._map.Center = new Location(59.334415, 18.110103);
            mapView._map.ZoomLevel = 5;
        }

        private async void Pushpin_OnMouseDown(object sender, RoutedEventArgs e)
        {
            var pushpin = (Pushpin) sender;
            await RefreshForecastAsync(pushpin.Name);
        }

        private async void SearchBox_SuggestionSelected(object sender, string e)
        {
            await RefreshForecastAsync(e);
        }

        private async Task RefreshForecastAsync(string cityName)
        {
            var city = _viewModel.Cities.FirstOrDefault<CityModel>(cityModel => cityModel.CityName == cityName);
            if (city != null)
            {
                await forecastListView.RefreshList(city);
                mapView._map.ZoomLevel = 8;
                mapView._map.Center = new Location(city.Latitude, city.Longitude);
                forecastListView.citySearchBoxView.searchBox.Text = cityName;
            }
        }
    }
}