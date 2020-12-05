A static library to fetch cpu and memory usage information for linux machines

## Usage

```csharp
using LinuxSystemStats;

double cpuPercentage = await Stats.GetCurrentCpuUsagePercentageAsync();
double cpuRoundedPercentage = await Stats.GetCurrentCpuUsagePercentageAsync(2); // 2dp

MemoryInformation memoryInformation = await Stats.GetMemoryInformationAsync(); 
MemoryInformation memoryInformationRounded = await Stats.GetMemoryInformationAsync(2); // 2dp
```

## How it works

The library uses these commands with /bin/bash to retrieve system statistics:

CPU ([source](https://stackoverflow.com/a/9229580/11089240))
```
awk '{u=$2+$4; t=$2+$4+$5; if (NR==1){u1=u; t1=t;} else print ($2+$4-u1) * 100 / (t-t1); }' \
<(grep 'cpu ' /proc/stat) <(sleep 1;grep 'cpu ' /proc/stat)
```

Memory ([source](https://stackoverflow.com/a/10586020/11089240))
```
free | grep Mem | awk '{print $2 " " $3 " " $4}'
```
