using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSVReadingService;
using WeatherApp.PresentationContext.Models;

namespace WeatherApp.Services.Model
{
    public class ForecastModel
    {
        private WeatherForecastService _forecastService;

        public ForecastModel()
        {
            _forecastService = new WeatherForecastService();
        }

        public async Task<List<ForecastDisplay>> GetForecastAsync(CityModel city)
        {
            var forecast = await _forecastService.GetForecastByCoordsAsync(city.Latitude,city.Longitude).ConfigureAwait(false);
            var forecastedResults = forecast.GetHighestTimeSeries();
            var result = new List<ForecastDisplay>();
            foreach (var forecastedResult in forecastedResults)
            {

                var forecastDisplay = new ForecastDisplay
                {
                    AirPressure = forecastedResult.Msl.ToString(),
                    Temperature = forecastedResult.T.ToString(),
                    WindSpeed = forecastedResult.Ws.ToString(),
                    WindDirection = forecastedResult.Wd.ToString(),
                    Date = forecastedResult.ValidTime.Date.ToString()
                };
                if (forecastedResult.ValidTime.Date == DateTime.Today)
                {
                    forecastDisplay.DayName = "Today";
                }
                else if (forecastedResult.ValidTime.Date == DateTime.Today.AddDays(1))
                {
                    forecastDisplay.DayName = "Tomorrow";
                }
                else
                {
                    forecastDisplay.DayName = forecastedResult.ValidTime.Date.DayOfWeek.ToString();
                }
                result.Add(forecastDisplay);
            }
            return result;
        }
    }
}
