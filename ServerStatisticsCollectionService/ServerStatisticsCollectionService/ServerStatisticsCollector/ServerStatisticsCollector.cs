using ServerStatisticsCollectionService.Models;
using ServerStatisticsCollectionService.ServerStatisticsCollector.Interfaces;
using System.Diagnostics;
using System.Management;

namespace ServerStatisticsCollectionService.ServerStatisticsCollector
{
    public class ServerStatisticsCollector : IServerStatisticsCollector
    {
        private readonly int _delaySeconds;
        public ServerStatisticsCollector(int delaySeconds)
        {
            _delaySeconds = Math.Max(delaySeconds-1, 0);
        }

        public ServerStatistics GetServerStatistics()
        {
            var stats = new ServerStatistics();

            //Avilable mem
            var performance = new PerformanceCounter("Memory", "Available MBytes");
            stats.AvailableMemory = performance.NextValue();

            //Memory Usage
            var objectQuery = new ObjectQuery("SELECT * FROM Win32_ComputerSystem");
            var searcher = new ManagementObjectSearcher(objectQuery);
            double totalMemoryMB = 0;
            foreach (var computerInfo in searcher.Get())
            {
                ulong totalPhysicalMemory = Convert.ToUInt64(computerInfo["TotalPhysicalMemory"]);
                totalMemoryMB += totalPhysicalMemory / (1024.0 * 1024);
            }
            stats.MemoryUsage = totalMemoryMB - stats.AvailableMemory;

            //CPU Usage
            PerformanceCounter cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            float maxCpuUsage = 0;
            for (int i = 0; i < 5; i++)
            {
                maxCpuUsage += Math.Max(cpuCounter.NextValue(), maxCpuUsage);
                Thread.Sleep(200);

            }

            stats.CpuUsage = maxCpuUsage;

            //Timestamp
            stats.Timestamp = DateTime.Now;

            Thread.Sleep(_delaySeconds * 1000);

            return stats;
        }
    }
}
