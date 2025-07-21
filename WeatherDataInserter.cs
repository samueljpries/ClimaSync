using System;
using System.Threading.Tasks;
using Dapper;
using Npgsql;

namespace WeatherIngestor
{
    public class WeatherDataInserter
    {
        private readonly WeatherIngestorConfig _config;
        private readonly ILogger _logger;

        public WeatherDataInserter(WeatherIngestorConfig config, ILogger logger)
        {
            _config = config;
            _logger = logger;
        }

        public async Task InsertAsync(WeatherApiResponse response)
        {
            if (response?.hourly?.time == null) return;

            using var db = new NpgsqlConnection(_config.ConnectionString);

            for (int i = 0; i < response.hourly.time.Length; i++)
            {
                await db.ExecuteAsync(@"
                    INSERT INTO weather_hourly (timestamp, latitude, longitude, temperature_2m, cloud_cover, direct_normal_irradiance)
                    VALUES (@timestamp, @lat, @lon, @temp, @cloud, @dni)
                    ON CONFLICT (timestamp) DO NOTHING;",
                    new
                    {
                        timestamp = DateTime.Parse(response.hourly.time[i]),
                        lat = response.latitude,
                        lon = response.longitude,
                        temp = response.hourly.temperature_2m[i],
                        cloud = response.hourly.cloud_cover[i],
                        dni = response.hourly.direct_normal_irradiance[i]
                    });
            }

            _logger.Info($"Inserted {response.hourly.time.Length} records into TimescaleDB.");
        }
    }
}
