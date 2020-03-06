using System.Runtime.InteropServices;

namespace SanderSade.Int64ToByteArray.Benchmarking
{
	public static class UsingStruct
	{
		public static byte[] GetBytesFromInt64(long val)
		{
			var longStuct = new LongStruct(val);
			return new[] { longStuct.B0, longStuct.B1, longStuct.B2, longStuct.B3, longStuct.B4, longStuct.B5, longStuct.B6, longStuct.B7 };
		}


		[StructLayout(LayoutKind.Explicit)]
		internal struct LongStruct
		{
			internal LongStruct(long int64) : this()
			{
				this.int64 = int64;
			}


			[FieldOffset(0)]
			internal long int64;

			[FieldOffset(0)]
			internal byte B0;

			[FieldOffset(1)]
			internal byte B1;

			[FieldOffset(2)]
			internal byte B2;

			[FieldOffset(3)]
			internal byte B3;

			[FieldOffset(4)]
			internal byte B4;

			[FieldOffset(5)]
			internal byte B5;

			[FieldOffset(6)]
			internal byte B6;

			[FieldOffset(7)]
			internal byte B7;
		}
	}
}
