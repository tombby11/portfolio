using System;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WeatherApp.Services.Model;

namespace WeatherApp.Services
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private const string _requestUriFormat =
            @"http://opendata-download-metfcst.smhi.se/api/category/pmp1.5g/version/1/geopoint/lat/{0}/lon/{1}/data.json";

        private readonly HttpClient _httpClient;
        private readonly NumberFormatInfo _numberFormatInfo;

        public WeatherForecastService()
        {
            var handler = new HttpClientHandler();
            _httpClient = new HttpClient(handler);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _numberFormatInfo = new NumberFormatInfo {NumberDecimalSeparator = "."};
        }

        public Forecast GetForecastByCoords(double latitude, double longitude)
        {
            var requestUri = string.Format(_numberFormatInfo, _requestUriFormat, latitude, longitude);
            var json = _httpClient.GetStringAsync(requestUri).Result;
            return JsonConvert.DeserializeObject<Forecast>(json);
        }


        public async Task<Forecast> GetForecastByCoordsAsync(double latitude, double longitude)
        {
            var requestUri = string.Format(_numberFormatInfo, _requestUriFormat, latitude, longitude);
            string json = "";
            try
            {
                json = await _httpClient.GetStringAsync(requestUri);
            }
            catch (Exception ex)
            {
                // propbably it is wrong long and lat so try to remove last few digits 

                latitude= Convert.ToDouble(latitude.ToString("N5"));
                longitude = Convert.ToDouble(longitude.ToString("N5"));
                 requestUri = string.Format(_numberFormatInfo, _requestUriFormat, latitude, longitude);
                json = await _httpClient.GetStringAsync(requestUri);
            }
          
            return JsonConvert.DeserializeObject<Forecast>(json);
        }
    }
}