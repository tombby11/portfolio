using System.Collections.Generic;
using System.Collections.ObjectModel;
using CSVReadingService;
using WeatherApp.PresentationContext.Models;
using WeatherApp.ViewModels;

namespace WeatherApp.PresentationContext.ViewModels
{
    public class CitiesViewModel : ViewModel
    {
        private readonly CityCsvService _model;
        private ObservableCollection<CityModel> _cities;
        private IEnumerable<string> _citiesNames;

        public CitiesViewModel()
        {
            _model = new CityCsvService();
            Cities = GetAllCities();
        }

        public ObservableCollection<string> CityNames { get; set; }

        public ObservableCollection<CityModel> Cities
        {
            get { return _cities; }
            set
            {
                _cities = value;
                NotifyPropertyChanged();
            }
        }

        public IEnumerable<string> CitiesNames
        {
            get { return _citiesNames; }
            set
            {
                _citiesNames = value;
                NotifyPropertyChanged();
            }
        }

        public City GetCity(string key)
        {
            City result = null;
            result = _model.GetCity(key);
            return result;
        }

        private ObservableCollection<CityModel> GetAllCities()
        {
            var result = new ObservableCollection<CityModel>();
            var resultNames = new ObservableCollection<string>();
            foreach (var city in _model.Cities)
            {
                var cityModel = new CityModel()
                {
                    CityName = city.Name,
                    Latitude = city.X,
                    Longitude = city.Y
                };
                result.Add(cityModel);
                resultNames.Add(cityModel.CityName);
            }
            CityNames = resultNames;
            return result;
        }
    }
}