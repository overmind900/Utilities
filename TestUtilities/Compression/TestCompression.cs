using Utilities;
using NUnit.Framework;
using System.Text;

namespace TestUtilities.Compression {
	[TestFixture]
	public partial class TestCompression {
		 
		[Test]
		public void Compression() {
			byte[] expected = Encoding.UTF8.GetBytes( m_XML );
			byte[] compressed = expected.Compress();
			Assert.IsNotEmpty( compressed );
			Assert.Less( compressed.Length, expected.Length );

			byte[] decompressed = compressed.Decompress();
			Assert.Greater( decompressed.Length, compressed.Length );
			Assert.AreEqual( expected, decompressed );
			Assert.AreEqual( m_XML, Encoding.UTF8.GetString( decompressed ) );
		}
	}
}