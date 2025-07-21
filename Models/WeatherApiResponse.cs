using System;

namespace WeatherIngestor.Models
{
    public class WeatherApiResponse
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
        public double generationtime_ms { get; set; }
        public int utc_offset_seconds { get; set; }
        public string timezone { get; set; }
        public string timezone_abbreviation { get; set; }
        public double elevation { get; set; }
        public HourlyUnits hourly_units { get; set; }
        public Hourly hourly { get; set; }
    }

    public class HourlyUnits
    {
        public string time { get; set; }
        public string temperature_2m { get; set; }
        public string cloud_cover { get; set; }
        public string direct_normal_irradiance { get; set; }
    }

    public class Hourly
    {
        public string[] time { get; set; }
        public double[] temperature_2m { get; set; }
        public double[] cloud_cover { get; set; }
        public double[] direct_normal_irradiance { get; set; }
    }
}
