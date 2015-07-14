using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
// ReSharper disable once CheckNamespace
namespace Utilities {
	/// <summary>
	/// Static extensions methods to Serialize objects into and from byte[]
	/// </summary>
	public static partial class Extension {
		
		/// <summary>
		/// Serialize objects into byte[], object must be [Serializable]
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="obj"></param>
		/// <returns></returns>
		public static byte[] ToByteArray<T>(this T obj ) {
			BinaryFormatter bf = new BinaryFormatter();
			using ( MemoryStream ms = new MemoryStream() ) {
				bf.Serialize( ms, obj );
				return ms.ToArray();
			}
		}

		/// <summary>
		/// Desterilize objects from byte[], object must be [Serializable] and should implement IConvertible
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="bytes"></param>
		/// <returns></returns>
		public static T FromByteArrayToObject<T>(this byte[] bytes ) {
			using ( MemoryStream memStream = new MemoryStream() ) {
				BinaryFormatter binForm = new BinaryFormatter();
				memStream.Write( bytes, 0, bytes.Length );
				memStream.Seek( 0, SeekOrigin.Begin );
				var obj = binForm.Deserialize( memStream );
				return (T) Convert.ChangeType( obj, typeof( T ) );
				
			}
		}

	}
}