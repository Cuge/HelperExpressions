``` ini

BenchmarkDotNet=v0.10.6, OS=Windows 7 SP1 (6.1.7601)
Processor=Intel Core i7-4771 CPU 3.50GHz (Haswell), ProcessorCount=8
Frequency=3418183 Hz, Resolution=292.5531 ns, Timer=TSC
dotnet cli version=1.0.4
  [Host]     : .NET Core 4.6.25211.01, 64bit RyuJIT [AttachedDebugger]
  DefaultJob : .NET Core 4.6.25211.01, 64bit RyuJIT


```
 |       Method |       Mean |     Error |    StdDev |
 |------------- |-----------:|----------:|----------:|
 |        Test1 | 16.9160 ns | 0.3638 ns | 0.6275 ns |
 |   TestDirect |  0.3105 ns | 0.0228 ns | 0.0202 ns |
 | TestBoxUnbox |  0.3333 ns | 0.0238 ns | 0.0211 ns |
