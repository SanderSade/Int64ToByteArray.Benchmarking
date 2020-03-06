namespace SanderSade.Int64ToByteArray.Benchmarking
{
	public class UsingUnsafeByteArray
	{
		public static byte[] GetBytesFromInt64(long val)
		{
			unsafe
			{
				var bytes = new byte[8];

				fixed (byte* b = bytes)
				{
					*(long*)b = val;
				}

				return bytes;
			}
		}
	}
}
