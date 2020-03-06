using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using SanderSade.Int64ToByteArray.Benchmarking;

namespace SanderSade.Int64ToByteArray.Benchmark
{
	[SimpleJob(RuntimeMoniker.NetCoreApp31)]
	[MarkdownExporterAttribute.GitHub]
	[MemoryDiagnoser]
	public class LongConversionBenchmark
	{
		internal const int Count = 1_000_000;
		private List<long> _cached;
		private List<long> Values => _cached ??= GetValues();


		private List<long> GetValues()
		{
			var values = new List<long>(Count);
			var random = new Random();

			for (var i = 0; i < Count; i++)
			{
				values.Add((long)(random.NextDouble() * long.MaxValue));
			}

			return values;
		}


		[Benchmark]
		public List<byte[]> UsingBinaryPrimitivesTest()
		{
			return Values.Select(UsingBinaryPrimitives.GetBytesFromInt64).ToList();
		}


		[Benchmark]
		public List<byte[]> UsingBitConverterTest()
		{
			return Values.Select(UsingBitConverter.GetBytesFromInt64).ToList();
		}


		[Benchmark]
		public List<byte[]> UsingStructTest()
		{
			return Values.Select(UsingStruct.GetBytesFromInt64).ToList();
		}


		[Benchmark]
		public List<byte[]> UsingUnsafeByteArrayTest()
		{
			return Values.Select(UsingUnsafeByteArray.GetBytesFromInt64).ToList();
		}
	}
}
