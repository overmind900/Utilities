using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
namespace Utilities.Comunication {
	public class TcpConnection {
		protected TcpClient m_Client;
		protected NetworkStream m_Stream;
		protected string m_Address;
		protected int m_Port;
		protected readonly string m_Id = Guid.NewGuid().ToString();

		public string Id {
			get { return m_Id; }
		}

		public bool Connected { get { return m_Client.Connected; } }


		protected TcpConnection() {
			m_Id = Guid.NewGuid().ToString();
		}

		public TcpConnection( string address, Int32 port ) 
			: this() {
			m_Address = address;
			m_Port = port;
			m_Client = new TcpClient();
			m_Client.Connect( m_Address, m_Port );
			m_Stream = m_Client.GetStream();
		}

		public TcpConnection( TcpClient client )
			: this() {
			m_Client = client;
			m_Stream = m_Client.GetStream();
		}

		public virtual void Close() {
			if ( m_Client.Connected ) {
				m_Client.Close();
			}
		}

		public virtual void Send( byte[] data ) {
			if ( !m_Client.Connected ) {
				Logging.Logger.LogError( "Trying to send when no connection available" );
			}
			//4 bytes containing the length of the data being sent
			byte[] package = BitConverter.GetBytes( data.Length ).Concat( data ).ToArray();
			m_Stream.Write( package, 0, package.Length );
		}

		public virtual Task SendAsync( byte[] data ) {
			if ( !m_Client.Connected ) {
				Logging.Logger.LogError( "Trying to send when no connection available" );
			}
			//4 bytes containing the length of the data being sent
			byte[] package = BitConverter.GetBytes( data.Length ).Concat( data ).ToArray();
			return m_Stream.WriteAsync( package, 0, package.Length );
		}

		public virtual byte[] Recieve() {
			if ( !m_Client.Connected ) {
				Logging.Logger.LogError( "Trying to receive when no connection available" );
			}
			//get the preamble Int32 is 4 bytes
			byte[] preamble = Read( 4 );
			int packageSize = BitConverter.ToInt32( preamble, 0 );
			//Read the message
			return Read( packageSize );

		}

		//TODO: RecieveAsync

		protected byte[] Read( int bytesToRead ) {
			List<Byte[]> recieveBuffer = new List<byte[]>();
			int recieved = 0;
			while ( recieved < bytesToRead ) {
				byte[] buffer = new byte[1000];
				int reed = 0;
				recieved += reed = m_Stream.Read( buffer, 0, Math.Min( 1000, bytesToRead - recieved ) );
				recieveBuffer.Add( buffer.Take( reed ).ToArray() );
			}
			return recieveBuffer.SelectMany( byteArr => byteArr ).ToArray();
		}
	}
}