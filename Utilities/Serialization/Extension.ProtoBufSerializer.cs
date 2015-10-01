using System.IO;
using ProtoBuf;

// ReSharper disable once CheckNamespace
namespace Utilities
{
	public static partial class Extension {

		public static byte[] ToProtobBuf<T>( this T obj ) {
			using ( MemoryStream memStream = new MemoryStream( ) ) {
				Serializer.Serialize( memStream, obj );
				return memStream.ToArray();
			}
		}

		public static T FromProtoBuf<T>( this byte[] data ) {
			using ( MemoryStream memStream = new MemoryStream( data ) ) {
				return Serializer.Deserialize<T>( memStream );
			}
		}


	}
}