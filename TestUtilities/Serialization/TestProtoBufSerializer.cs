using NUnit.Framework;
using Utilities;
namespace TestUtilities.Serialization {
	public class TestProtoBufSerializer {
		
		[Test]
		[TestCase ("Hello")]
		[TestCase (5)]
		[TestCase (4L)]
		[TestCase (5.0D)]
		[TestCase (4.0F)]
		[TestCase (true)]
		public void ProtoBuf_Simple_obj<T>( T obj) {
			byte[] serilized = obj.ToProtobBuf();
			Assert.IsNotEmpty( serilized );
			T decerilised = serilized.FromProtoBuf<T>();
			Assert.AreEqual( obj, decerilised );
			Assert.AreNotSame( obj, decerilised );
		}
		
	}
}