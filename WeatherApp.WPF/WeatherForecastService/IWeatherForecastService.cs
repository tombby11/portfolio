namespace WeatherApp.Services
{
  using Model;
  
  public interface IWeatherForecastService
  {
    Forecast GetForecastByCoords(double latitude, double longitude);
  }
}