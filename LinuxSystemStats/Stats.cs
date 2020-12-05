using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace LinuxSystemStats
{
    public static class Stats
    {
        public static async Task<double> GetCurrentCpuUsagePercentageAsync(int decimals = -1)
        {
            string result = await RunCommandAsync("awk '{u=$2+$4; t=$2+$4+$5; if (NR==1){u1=u; t1=t;} else print ($2+$4-u1) * 100 / (t-t1); }' <(grep 'cpu ' /proc/stat) <(sleep 1;grep 'cpu ' /proc/stat)");
            double value = double.Parse(result);
            if (decimals >= 0) value = Math.Round(value, decimals);
            return value;
        }

        public static async Task<MemoryInformation> GetMemoryInformationAsync(int decimals = -1)
        {
            string result = await RunCommandAsync("free | grep Mem | awk '{print $2 \" \" $3 \" \" $4}'");
            return new MemoryInformation(result, decimals);
        }

        private static async Task<string> RunCommandAsync(string command)
        {
            string escapedCommand = command.Replace("\"", "\\\"");

            Process process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = $"-c \"{escapedCommand}\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.Start();
            string result = await process.StandardOutput.ReadToEndAsync();
            process.WaitForExit();

            return result;
        }
    }
}
