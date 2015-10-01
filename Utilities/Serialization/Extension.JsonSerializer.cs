using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml;

// ReSharper disable once CheckNamespace
namespace Utilities
{
	public static partial class Extension {

		public static string SerializeToJson( this object obj ) {
			var jsonSerializer = new DataContractJsonSerializer( obj.GetType() );
			string returnValue = "";
			using ( var memoryStream = new MemoryStream() ) {
				using ( var xmlWriter = JsonReaderWriterFactory.CreateJsonWriter( memoryStream , Encoding.UTF8, false, true) ) {
					jsonSerializer.WriteObject( xmlWriter, obj );
					xmlWriter.Flush();
					returnValue = Encoding.UTF8.GetString( memoryStream.GetBuffer(), 0, (int)memoryStream.Length );
				}
			}
			return returnValue;
		}

		public static T DeserializeFromJson<T>( this string json ) {
			T returnValue;
			using ( var memoryStream = new MemoryStream() ) {
				byte[] jsonBytes = Encoding.UTF8.GetBytes( json );
				memoryStream.Write( jsonBytes, 0, jsonBytes.Length );
				memoryStream.Seek( 0, SeekOrigin.Begin );
				using ( var jsonReader = JsonReaderWriterFactory.CreateJsonReader( memoryStream, Encoding.UTF8, XmlDictionaryReaderQuotas.Max, null ) ) {
					var serializer = new DataContractJsonSerializer( typeof( T ) );
					returnValue = (T)serializer.ReadObject( jsonReader );

				}
			}
			return returnValue;
		}
    }
}
