using System;
using System.Threading.Tasks;
using LinuxSystemStats;

namespace Test
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            double cpu = await Stats.GetCurrentCpuUsagePercentageAsync(2);
            MemoryInformation memory = await Stats.GetMemoryInformationAsync(2);
            
            Console.WriteLine($"CPU: {cpu}%");
            Console.WriteLine($"Memory: {memory.UsedGigabytes}/{memory.TotalGigabytes} ({memory.FreeGigabytes} free)");
        }
    }
}
