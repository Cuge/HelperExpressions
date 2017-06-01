``` ini

BenchmarkDotNet=v0.10.6, OS=Windows 7 SP1 (6.1.7601)
Processor=Intel Core i7-4771 CPU 3.50GHz (Haswell), ProcessorCount=8
Frequency=3418183 Hz, Resolution=292.5531 ns, Timer=TSC
dotnet cli version=1.0.4
  [Host]     : .NET Core 4.6.25211.01, 64bit RyuJIT
  DefaultJob : .NET Core 4.6.25211.01, 64bit RyuJIT


```
 |          Method |          Mean |       Error |      StdDev |   Scaled | ScaledSD |
 |---------------- |--------------:|------------:|------------:|---------:|---------:|
 | TestDelegateGet | 12,277.760 ns | 238.3868 ns | 244.8057 ns | 2,086.71 |    67.57 |
 |           Test1 |    115.554 ns |  11.9301 ns |  15.9264 ns |    19.64 |     2.70 |
 |      TestDirect |      5.888 ns |   0.1457 ns |   0.1559 ns |     1.00 |     0.00 |
