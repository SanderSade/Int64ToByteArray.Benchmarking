using System;
using System.Buffers.Binary;

namespace SanderSade.Int64ToByteArray.Benchmarking
{
	public static class UsingBinaryPrimitives
	{
		public static byte[] GetBytesFromInt64(long val)
		{
			var destination = new Span<byte>(new byte[8]);
			BinaryPrimitives.WriteInt64LittleEndian(destination, val);
			return destination.ToArray();
		}
	}
}
