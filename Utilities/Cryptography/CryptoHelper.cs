using System.IO;
using System.Security.Cryptography;
using System.Text;
using Utilities.Serialization;

namespace Utilities.Cryptography {
	public static class CryptoHelper {

		public static T Decrypt<T>( byte[] data, ICryptoTransform decryptor ) {
			return Encoding.UTF8.GetBytes( Decrypt( data, decryptor ) ).FromByteArrayToObject<T>();
		}

		public static string Decrypt( byte[] data, ICryptoTransform decryptor ) {
			using ( MemoryStream msDecrypt = new MemoryStream( data ) ) {
				using ( CryptoStream csDecrypt = new CryptoStream( msDecrypt, decryptor, CryptoStreamMode.Read ) ) {
					using ( StreamReader srDecrypt = new StreamReader( csDecrypt ) ) {
						// Read the decrypted bytes from the decrypting stream 
						// and place them in a string.
						return srDecrypt.ReadToEnd();
					}
				}
			}
		}

		public static byte[] Encrypt<T>( T data, ICryptoTransform encryptor ) {
			//must be BinarySerializable
			return Encrypt( Encoding.UTF8.GetString(data.ToByteArray()), encryptor );
		}

		public static byte[] Encrypt( string data, ICryptoTransform encryptor ) {
			using ( MemoryStream msEncrypt = new MemoryStream() ) {
				using ( CryptoStream csEncrypt = new CryptoStream( msEncrypt, encryptor, CryptoStreamMode.Write ) ) {
					using ( StreamWriter swEncrypt = new StreamWriter( csEncrypt ) ) {
						//Write all data to the stream.
						swEncrypt.Write( data );
					}
					return msEncrypt.ToArray();
				}
			}
		}
	}
}
