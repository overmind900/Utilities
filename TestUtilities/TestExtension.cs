using System.Collections.Generic;
using NUnit.Framework;
using Utilities;
using System.Linq;
namespace TestUtilities {
	[TestFixture]
	public class TestExtension {
		
 		[Test]
		public void TestForeach() {
			int[] ia = { 1, 1, 1, 1 };
			int count = 0;
			ia.ForEach( i => count += i );
			Assert.AreEqual( 4, count );
		}

		[Test]
		public void TestTimes() {
			int i = 0;
			10.Times(() => i++ );
			Assert.AreEqual( 10, i );
		}

	}
}