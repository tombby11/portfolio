using System;
using System.Collections.Generic;
using System.Linq;
using WeatherForecastService;

namespace WeatherApp.Services.Model
{
    public class Forecast
    {
        public string CityName { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public DateTimeOffset ReferenceTime { get; set; }
        public TimeSeries[] TimeSeries { get; set; }

        public override string ToString()
        {
            var name = ReferenceTime.DayOfWeek.ToString();
            return string.Format("ReferenceTime={0}, Latitude={1}, Longitude={2}", ReferenceTime, Lat, Lon);
        }

        public List<TimeSeries> GetHighestTimeSeries()
        {
            // Assuming that 12 is among the highest temps 
            var series = TimeSeries.Where(t => t.ValidTime.Hour == 12).ToList();

            var today =
                TimeSeries.Where(t => t.ValidTime.Date == DateTime.Today.Date && t.ValidTime.Hour == 12).ToList();
            if (today.Any())
            {
                //there is today's record 
            }
            else
            {
                //there is no record for today at 12 so we take the record now and put it as today's 
                var todaysRecord =
                    TimeSeries.FirstOrDefault(
                        t => t.ValidTime.Date == DateTime.Today.Date && t.ValidTime.Hour == DateTime.Now.Hour);
                series.Insert(0, todaysRecord);
            }
            return series;
        }
    }
}