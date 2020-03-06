using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SanderSade.Int64ToByteArray.Benchmarking;

namespace Int64ToByteArray.Benchmarking.Tests
{
	[TestClass]
	public class Validations
	{
		[TestMethod]
		public void ValidateOne()
		{
			var l = 1929395416L;
			var bp = UsingBinaryPrimitives.GetBytesFromInt64(l);
			var bc = UsingBitConverter.GetBytesFromInt64(l);
			var c = UsingStruct.GetBytesFromInt64(l);
			var us = UsingUnsafeByteArray.GetBytesFromInt64(l);

			Assert.AreEqual(bp.Length, 8);
			Assert.AreEqual(bc.Length, 8);
			Assert.AreEqual(c.Length, 8);
			Assert.AreEqual(us.Length, 8);

			for (var i = 0; i < 8; i++)
			{
				Assert.AreEqual(bp[i], bc[i]);
				Assert.AreEqual(bp[i], c[i]);
				Assert.AreEqual(bp[i], us[i]);
				Trace.Write(bp[i].ToString("X"));
			}
		}
	}
}
