using System;

namespace WeatherForecastService
{
  public class TimeSeries
  {
    public DateTimeOffset ValidTime { get; set; }
    public double T { get; set; }
    public double Tcc { get; set; }
    public double Lcc { get; set; }
    public double Mcc { get; set; }
    public double Hcc { get; set; }
    public double Tstm { get; set; }
    public double R { get; set; }
    public double Vis { get; set; }
    public double Gust { get; set; }
    public double Pit { get; set; }
    public double Pis { get; set; }
    public double Pcat { get; set; }
    public double Msl { get; set; }
    public double Wd { get; set; }        
    public double Ws { get; set; }

    public override string ToString()
    {
      return string.Format(@"Time={0}, Temperature={1} C, WindVelocity={2} m/s, WindDirection={3} Deg", ValidTime, T, Ws, Wd);
    }
  }
}