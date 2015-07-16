using System.IO;
using System.IO.Compression;

// ReSharper disable once CheckNamespace
namespace Utilities {
	public partial class Extension {

		public static byte[] Compress( this byte[] data ) {
			using ( MemoryStream outStream = new MemoryStream() ) {
				using ( GZipStream zipStream = new GZipStream( outStream, CompressionMode.Compress ) ) {
					using ( MemoryStream inStream = new MemoryStream( data ) ) {
						inStream.CopyTo( zipStream );
					}
				}
				return outStream.ToArray();
			}
		}

		public static byte[] Decompress( this byte[] data ) {
			using ( MemoryStream inStream = new MemoryStream( data ) ) {
				using ( GZipStream zipStream = new GZipStream( inStream, CompressionMode.Decompress ) ) {
					using ( MemoryStream outStream = new MemoryStream() ) {
						zipStream.CopyTo( outStream );
						return outStream.ToArray();
					}
				}
			}
		}
	}
}