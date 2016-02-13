using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CSVReadingService;
using WeatherApp.PresentationContext.Models;
using WeatherApp.PresentationContext.ViewModels;
using WeatherApp.Services.Model;

namespace WeatherApp.ViewModels
{
    public class ForecastViewModel : ViewModel
    {
        private readonly ForecastModel _model;
        private ObservableCollection<ForecastDisplay> _forecastDisplays;

        public ForecastViewModel()
        {
            _model = new ForecastModel();
            ForecastDisplays = new ObservableCollection<ForecastDisplay>();
        }

        public ObservableCollection<ForecastDisplay> ForecastDisplays
        {
            get { return _forecastDisplays; }
            set
            {
                _forecastDisplays = value;
                NotifyPropertyChanged();
            }
        }


        public async Task<List<ForecastDisplay>> GetForecastDisplays(CityModel city)
        {
            return await _model.GetForecastAsync(city);
        }

        public async Task RefreshForecastListAsync(CityModel city)
        {

            var forecasts = await GetForecastDisplays(city);
            ForecastDisplays.Clear();
            foreach (var forecastDisplay in forecasts)
            {
             
                ForecastDisplays.Add(forecastDisplay);
            }
        }
    }
}