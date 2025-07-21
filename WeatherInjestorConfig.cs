using System;

namespace WeatherIngestor
{
    public class WeatherIngestorConfig
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ConnectionString { get; set; }
    }
}
