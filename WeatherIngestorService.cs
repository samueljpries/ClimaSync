using System;
using System.Threading.Tasks;

namespace WeatherIngestor
{
    public class WeatherIngestorService
    {
        private readonly WeatherIngestorConfig _config;
        private readonly WeatherApiFetcher _fetcher;
        private readonly WeatherDataInserter _inserter;
        private readonly ILogger _logger;

        public WeatherIngestorService(
            WeatherIngestorConfig config,
            WeatherApiFetcher fetcher,
            WeatherDataInserter inserter,
            ILogger logger)
        {
            _config = config;
            _fetcher = fetcher;
            _inserter = inserter;
            _logger = logger;
        }

        public async Task RunAsync()
        {
            for (DateTime current = _config.StartDate; current <= _config.EndDate; current = current.AddMonths(1))
            {
                DateTime monthEnd = new DateTime(current.Year, current.Month, DateTime.DaysInMonth(current.Year, current.Month));
                if (monthEnd > _config.EndDate) monthEnd = _config.EndDate;

                try
                {
                    var data = await _fetcher.FetchMonthAsync(current, monthEnd);
                    await _inserter.InsertAsync(data);
                }
                catch (Exception ex)
                {
                    _logger.Error($"Error processing {current:yyyy-MM}: {ex.Message}");
                }
            }

            _logger.Info("Weather ingestion complete.");
        }
    }
}
