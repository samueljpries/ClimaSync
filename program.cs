using System;
using System.Threading.Tasks;

namespace WeatherIngestor
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var config = new WeatherIngestorConfig
            {
                Latitude = 52.52,
                Longitude = 13.41,
                StartDate = new DateTime(2024, 1, 1),
                EndDate = new DateTime(2024, 12, 31),
                ConnectionString = "Host=localhost;Port=5433;Username=postgres;Password=test;Database=weather"
            };

            var logger = new ConsoleLogger();
            var fetcher = new WeatherApiFetcher(config, logger);
            var inserter = new WeatherDataInserter(config, logger);
            var ingestor = new WeatherIngestorService(config, fetcher, inserter, logger);

            await ingestor.RunAsync();
        }
    }
}
