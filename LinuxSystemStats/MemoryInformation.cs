using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinuxSystemStats
{
    public class MemoryInformation
    {
        public double TotalGigabytes { get; }
        public double UsedGigabytes { get; }
        public double FreeGigabytes { get; }

        internal MemoryInformation(string result, int decimals)
        {
            double[] values = result.Split(' ').Select(double.Parse).ToArray();

            TotalGigabytes = values[0] / 1024 / 1024;
            UsedGigabytes = values[1] / 1024 / 1024;
            FreeGigabytes = values[2] / 1024 / 1024;

            if (decimals >= 0)
            {
                TotalGigabytes = Math.Round(TotalGigabytes, decimals);
                UsedGigabytes = Math.Round(UsedGigabytes, decimals);
                FreeGigabytes = Math.Round(FreeGigabytes, decimals);
            }
        }
    }
}
