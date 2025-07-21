using System;

namespace WeatherIngestor
{
    public interface ILogger
    {
        void Info(string message);
        void Error(string message);
    }

    public class ConsoleLogger : ILogger
    {
        public void Info(string message) => Console.WriteLine($"[INFO] {DateTime.Now}: {message}");
        public void Error(string message) => Console.WriteLine($"[ERROR] {DateTime.Now}: {message}");
    }
}
