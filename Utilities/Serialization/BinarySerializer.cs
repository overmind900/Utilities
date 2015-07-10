using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
namespace Utilities.Serialization {
	public static class BinarySerializer {
		
		public static byte[] ToByteArray<T>(this T obj ) {
			BinaryFormatter bf = new BinaryFormatter();
			using ( MemoryStream ms = new MemoryStream() ) {
				bf.Serialize( ms, obj );
				return ms.ToArray();
			}
		}

		public static T FromByteArrayToObject<T>(this byte[] arrBytes ) {
			using ( MemoryStream memStream = new MemoryStream() ) {
				BinaryFormatter binForm = new BinaryFormatter();
				memStream.Write( arrBytes, 0, arrBytes.Length );
				memStream.Seek( 0, SeekOrigin.Begin );
				var obj = binForm.Deserialize( memStream );
				return (T) Convert.ChangeType( obj, typeof( T ) );
				
			}
		}

	}
}