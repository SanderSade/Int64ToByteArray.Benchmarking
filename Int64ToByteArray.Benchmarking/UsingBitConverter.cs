using System;

namespace SanderSade.Int64ToByteArray.Benchmarking
{
	public static class UsingBitConverter
	{
		public static byte[] GetBytesFromInt64(long val)
		{
			return BitConverter.GetBytes(val);
		}
	}
}
