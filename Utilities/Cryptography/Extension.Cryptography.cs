using System.IO;
using System.Security.Cryptography;
using System.Text;
// ReSharper disable once CheckNamespace
namespace Utilities {
	/// <summary>
	/// Helper methods to encrypt/decrypt objects
	/// </summary>
	public static partial class Extension {

		/// <summary>
		/// Decrypts an object with the provided ICryptoTransform
		/// </summary>
		/// <typeparam name="T">a [Serializable] object</typeparam>
		/// <param name="data">System.byte[]</param>
		/// <param name="decryptor">System.Security.Cryptography.ICryptoTransform</param>
		/// <returns>T</returns>
		public static T Decrypt<T>(this byte[] data, ICryptoTransform decryptor ) {
			return Encoding.UTF8.GetBytes( data.Decrypt( decryptor ) ).FromByteArray<T>();
		}

		/// <summary>
		/// Encrypts an object with the provided ICryptoTransform
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="data">a [Serializable] object</param>
		/// <param name="encryptor">System.Security.Cryptography.ICryptoTransform</param>
		/// <returns>System.byte[]</returns>
		public static byte[] Encrypt<T>(this T data, ICryptoTransform encryptor ) {
			//must be BinarySerializable
			return Encoding.UTF8.GetString(data.ToByteArray()).Encrypt( encryptor );
		}

		/// <summary>
		/// Decrypts string data with the provided ICryptoTransform
		/// </summary>
		/// <param name="data">System.byte[]</param>
		/// <param name="decryptor">System.Security.Cryptography.ICryptoTransform</param>
		/// <returns>System.String</returns>
		public static string Decrypt(this byte[] data, ICryptoTransform decryptor ) {
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

		/// <summary>
		/// Encrypts string data with the provided ICryptoTransform
		/// </summary>
		/// <param name="data">System.String</param>
		/// <param name="encryptor">System.Security.Cryptography.ICryptoTransform</param>
		/// <returns>System.byte[]</returns>
		public static byte[] Encrypt(this string data, ICryptoTransform encryptor ) {
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
