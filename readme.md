## Int64ToByteArray.Benchmarking

I needed to get Int64 (long) values as byte array on a program hotpath (roughly 20 000...30 000 calls/second, possibly more in the future).


As .NET has quite a few ways to get number as a byte array, I decided to do some quick benhcmarking to see what option should I use.


### Implementations

* [BinaryPrimitives.WriteInt64LittleEndian()](https://github.com/SanderSade/Int64ToByteArray.Benchmarking/blob/1e118141a95d063f56d2f5bd3696f330ba2b522f/Int64ToByteArray.Benchmarking/UsingBinaryPrimitives.cs#L8), using Span
* [BitConverter.GetBytes()](https://github.com/SanderSade/Int64ToByteArray.Benchmarking/blob/1e118141a95d063f56d2f5bd3696f330ba2b522f/Int64ToByteArray.Benchmarking/UsingBitConverter.cs#L7) - old, trusty, the "default" way
* [Byte array in unsafe context](https://github.com/SanderSade/Int64ToByteArray.Benchmarking/blob/1e118141a95d063f56d2f5bd3696f330ba2b522f/Int64ToByteArray.Benchmarking/UsingUnsafeByteArray.cs#L7)
* [Explicitly laid out struct](https://github.com/SanderSade/Int64ToByteArray.Benchmarking/blob/1e118141a95d063f56d2f5bd3696f330ba2b522f/Int64ToByteArray.Benchmarking/UsingStruct.cs#L7)


### Results

.NET Core 3.1.1, using [BenchmarkDotNet](https://benchmarkdotnet.org/) v0.12. One million iterations inside each test method.

``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.18363
Intel Core i7-8650U CPU 1.90GHz (Kaby Lake R), 1 CPU, 4 logical and 4 physical cores
.NET Core SDK=3.1.101
  [Host]     : .NET Core 3.1.1 (CoreCLR 4.700.19.60701, CoreFX 4.700.19.60801), X64 RyuJIT
  Job-USBEJN : .NET Core 3.1.1 (CoreCLR 4.700.19.60701, CoreFX 4.700.19.60801), X64 RyuJIT

Runtime=.NET Core 3.1  

```
|                    Method |      Mean |    Error |    StdDev |    Median |      Gen 0 |     Gen 1 |     Gen 2 | Allocated |
|-------------------------- |----------:|---------:|----------:|----------:|-----------:|----------:|----------:|----------:|
| UsingBinaryPrimitivesTest | 274.47 ms | 5.605 ms | 15.248 ms | 270.12 ms | 11000.0000 | 3666.6667 | 1000.0000 |  68.66 MB |
|     UsingBitConverterTest | 100.67 ms | 1.982 ms |  2.036 ms | 100.19 ms |  5666.6667 | 2333.3333 |  666.6667 |  38.15 MB |
|           UsingStructTest |  95.81 ms | 1.903 ms |  2.191 ms |  95.13 ms |  5600.0000 | 2200.0000 |  600.0000 |  38.15 MB |
|  UsingUnsafeByteArrayTest | 105.14 ms | 2.217 ms |  4.377 ms | 103.77 ms |  5666.6667 | 2333.3333 |  666.6667 |  38.15 MB |


### Discussion

I threw the BinaryPrimitives in just for curiosity, as I have seen people recommending it over BitConverter. Perhaps I implemented or used it wrong, but it is almost 3x slower then the fastest, and uses twice as much memory (which is expected, as it has twice as many allocations).

Three other methods are more or less equal. Struct is sligtly faster, and unsafe byte array slightly slower than BitConverter - but for everyday purposes there are no differences.
