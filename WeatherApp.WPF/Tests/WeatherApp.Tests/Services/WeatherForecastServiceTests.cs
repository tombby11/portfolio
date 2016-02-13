using System;
using System.Threading.Tasks;
using CSVReadingService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeatherApp.Services;

namespace WeatherApp.Tests.Services
{
    [TestClass]
    public class WeatherForecastServiceTests
    {
        [TestMethod]
        public void Should_return_forecast_for_coords()
        {
            // Arrange
            var service = new WeatherForecastService();
            const double latitude = 59.334415;
            const double longitude = 18.110103;

            // Act
            var forecast = service.GetForecastByCoords(latitude, longitude);
 
            // Assert
            Assert.IsNotNull(forecast);
        }


        [TestMethod]
        public async Task Test_GetForecastByCoordsAsync()
        {
            // Arrange
            var service = new WeatherForecastService();
            const double latitude = 59.334415;
            const double longitude = 18.110103;

            // Act
            var forecast = await service.GetForecastByCoordsAsync(latitude, longitude);


            // Assert
            Assert.IsNotNull(forecast);
        }

        [TestMethod]
        public async Task Test_GetForecast_City_Async()
        {
            // Arrange
            var service = new WeatherForecastService();
            const double latitude = 59.334415;
            const double longitude = 18.110103;
            var city = new City {X = latitude, Y = longitude, Name = "Stockholm"};


            // Act
            var forecast = await service.GetForecastAsync(city);


            // Assert
            Assert.AreEqual(forecast.CityName , city.Name);
        }
    }
}