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

		/// <summary>
		/// A guid string uniquely identifying this connection
		/// </summary>
		public string Id {
			get { return m_Id; }
		}

		/// <summary>
		/// True if TcpClient is connected, otherwise false
		/// </summary>
		public bool Connected { get { return m_Client != null && m_Client.Connected; } }

		/// <summary>
		/// Initializes a new instance of the TcpConnection class. 
		/// </summary>
		protected TcpConnection() {
			m_Id = Guid.NewGuid().ToString();
		}

		/// <summary>
		/// Initializes a new instance of the TcpConnection class and connects to the address and port provided
		/// </summary>
		/// <param name="address">System.String</param>
		/// <param name="port">System.Int32</param>
		public TcpConnection( string address, Int32 port ) 
			: this() {
			m_Address = address;
			m_Port = port;
			m_Client = new TcpClient();
			m_Client.Connect( m_Address, m_Port );
			m_Stream = m_Client.GetStream();
		}

		/// <summary>
		/// Initializes a new instance of the TcpConnection class with the connected TcpClient
		/// </summary>
		/// <param name="client">System.Net.Sockets.TcpClient</param>
		public TcpConnection( TcpClient client )
			: this() {
			m_Client = client;
			m_Stream = m_Client.GetStream();
		}

		/// <summary>
		/// Opens a Tcp Connection if the TcpConnection class was created with the parameter less constructor
		/// </summary>
		/// <param name="address">System.String</param>
		/// <param name="port">System.Int32</param>
		public void Open( string address, Int32 port ) {
			if ( Connected ) {
				Logging.Logger.LogWarning( "Connection is already open" );
				return; 
			}
			m_Address = address;
			m_Port = port;
			m_Client = new TcpClient();
			m_Client.Connect( m_Address, m_Port );
			m_Stream = m_Client.GetStream();
		}

		/// <summary>
		/// Closes the current stream and releases any resources.
		/// </summary>
		public virtual void Close() {
			if( !m_Client.Connected ) return;
			m_Client.Close();
			m_Stream.Close();
		}

		/// <summary>
		/// Sends a sequence of bytes to the current stream.
		/// </summary>
		/// <param name="data">System.byte[]</param>
		public virtual void Send( byte[] data ) {
			if ( !m_Client.Connected ) {
				Logging.Logger.LogError( "Trying to send when no connection available" );
			}
			//4 bytes containing the length of the data being sent
			byte[] package = BitConverter.GetBytes( data.Length ).Concat( data ).ToArray();
			m_Stream.Write( package, 0, package.Length );
		}

		/// <summary>
		/// Asynchronously sends a sequence of bytes to the current stream.
		/// </summary>
		/// <param name="data">System.byte[]</param>
		/// <returns></returns>
		public virtual Task SendAsync( byte[] data ) {
			if ( !m_Client.Connected ) {
				Logging.Logger.LogError( "Trying to send when no connection available" );
			}
			//4 bytes containing the length of the data being sent
			byte[] package = BitConverter.GetBytes( data.Length ).Concat( data ).ToArray();
			return m_Stream.WriteAsync( package, 0, package.Length );
		}

		/// <summary>
		/// Reads data from the System.Net.Sockets.NetworkStream
		/// </summary>
		/// <returns>System.byte[]</returns>
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

		/// <summary>
		/// Reads data from the System.Net.Sockets.NetworkStream
		/// </summary>
		/// <param name="bytesToRead">The number of bytes to read from the System.Net.Sockets.NetworkStream</param>
		/// <returns>System.byte[]</returns>
		protected byte[] Read( int bytesToRead ) {
			List<Byte[]> recieveBuffer = new List<byte[]>();
			int recieved = 0;
			while ( recieved < bytesToRead ) {
				byte[] buffer = new byte[1000];
				int reed;
				recieved += reed = m_Stream.Read( buffer, 0, Math.Min( 1000, bytesToRead - recieved ) );
				recieveBuffer.Add( buffer.Take( reed ).ToArray() );
			}
			return recieveBuffer.SelectMany( byteArr => byteArr ).ToArray();
		}
	}
}