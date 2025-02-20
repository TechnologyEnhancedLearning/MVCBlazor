using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Package.Shared.Entities.T_Entities
{
  public class T_WeatherForecast
  {
    public DateOnly Date { get; set; }
    public int TemperatureC { get; set; }
    public string? Summary { get; set; }
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

     public List<T_WeatherReadingLocation> LocationsWeatherAveragedFrom { get; set; } = new List<T_WeatherReadingLocation>() 
     { 
        new T_WeatherReadingLocation(){ LocationName = "Just want a complex object 1" },
        new T_WeatherReadingLocation(){ LocationName = "Just want a complex object 2" }
     };
  }
    public class T_WeatherReadingLocation
    {
        public string LocationName { get; set; } = "Unset";
    }
}
