using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WeatherForecastService.Test
{
    [TestClass]
    public class WeatherForecastServiceTests
    {
        [TestMethod]
        public void Should_return_forecast_for_coords()
        {
            // Arrange
            var service = new WeatherApp.Services.WeatherForecastService();
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
            var service = new WeatherApp.Services.WeatherForecastService();
            const double latitude = 59.334415;
            const double longitude = 18.110103;

            // Act
            var forecast = await service.GetForecastByCoordsAsync(latitude, longitude);


            // Assert
            Assert.IsNotNull(forecast);
        }


        [TestMethod]
        public async Task Test_GetForecastByCoordsAsync_Malmo()
        {
            // Arrange
            var service = new WeatherApp.Services.WeatherForecastService();
            const double latitude = 55.602840;
            const double longitude = 12.998944;

            // Act
            var forecast = await service.GetForecastByCoordsAsync(latitude, longitude);


            // Assert
            Assert.IsNotNull(forecast);
        }
    }
}