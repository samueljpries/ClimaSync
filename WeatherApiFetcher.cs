using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Linq;

namespace WeatherIngestor
{
    public class WeatherApiFetcher
    {
        private readonly WeatherIngestorConfig _config;
        private readonly ILogger _logger;
        private readonly HttpClient _client = new();

        public WeatherApiFetcher(WeatherIngestorConfig config, ILogger logger)
        {
            _config = config;
            _logger = logger;
        }

        public async Task<WeatherApiResponse> FetchMonthAsync(DateTime start, DateTime end)
        {
            var parameters = new Dictionary<string, string>
            {
                ["latitude"] = _config.Latitude.ToString(),
                ["longitude"] = _config.Longitude.ToString(),
                ["start_date"] = start.ToString("yyyy-MM-dd"),
                ["end_date"] = end.ToString("yyyy-MM-dd"),
                ["hourly"] = "temperature_2m,cloud_cover,direct_normal_irradiance",
                ["temperature_unit"] = "fahrenheit",
                ["wind_speed_unit"] = "mph",
                ["precipitation_unit"] = "inch",
                ["timezone"] = "auto"
            };

            string url = "https://archive-api.open-meteo.com/v1/archive?" + string.Join("&", parameters.Select(kv => $"{kv.Key}={kv.Value}"));

            _logger.Info($"Requesting data for {start:yyyy-MM-dd} to {end:yyyy-MM-dd}");
            var response = await _client.GetStringAsync(url);
            return JsonSerializer.Deserialize<WeatherApiResponse>(response);
        }
    }

    public class WeatherApiResponse
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
        public Hourly hourly { get; set; }
    }

    public class Hourly
    {
        public string[] time { get; set; }
        public double[] temperature_2m { get; set; }
        public double[] cloud_cover { get; set; }
        public double[] direct_normal_irradiance { get; set; }
    }
}
