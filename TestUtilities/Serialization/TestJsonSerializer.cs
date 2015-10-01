
using NUnit.Framework;
using Utilities;

namespace TestUtilities.Serialization {
	[TestFixture]
	public class TestJsonSerializer {

		[Test]
		[TestCase( "Hello" )]
		[TestCase( 5 )]
		[TestCase( 4L )]
		[TestCase( 5.0D )]
		[TestCase( 4.0F )]
		[TestCase( true )]
		public void Json_Simple_obj<T>( T obj ) {
			string serilized = obj.ToJson();
			Assert.IsNotEmpty( serilized );
			T decerilised = serilized.FromJson<T>();
			Assert.AreEqual( obj, decerilised );
			Assert.AreNotSame( obj, decerilised );
		}
	}
}