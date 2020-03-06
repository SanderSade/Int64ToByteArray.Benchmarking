using System;
using BenchmarkDotNet.Running;

namespace SanderSade.Int64ToByteArray.Benchmark
{
	public class Program
	{
		public static void Main()
		{
			var summary = BenchmarkRunner.Run<LongConversionBenchmark>();
			Console.Write(summary.AllRuntimes);

			Console.ReadKey();
		}
	}
}
