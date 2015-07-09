using System;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Utilities.Logging;
using System.Collections.Generic;
namespace Utilities.Comunication {
	public class TCPServer {
		
		private TcpListener m_Listener;
		private static ConcurrentDictionary<string, TcpClient> m_Clients = new ConcurrentDictionary<string, TcpClient>();

		public event EventHandler TCPClinetConnected;

		public TCPServer( string ipaddress , int port) {
			m_Listener = new TcpListener( IPAddress.Parse( ipaddress ), port );
		}

		public void Start() {
			m_Listener.Start();
			Logger.LogInfo( string.Format( "Waiting for a connection on {0}:{1}", ipaddress, port ) );
	

			// Accept the connection.  
			m_Listener.BeginAcceptTcpClient( new AsyncCallback( DoAcceptTcpClientCallback ), m_Listener );
			
		}

		private void DoAcceptTcpClientCallback( IAsyncResult ar ) {
			// Get the listener that handles the client request.
			TcpListener listener = (TcpListener)ar.AsyncState;

			// End the operation and display the received data on  
			// the console.
			TcpClient client = listener.EndAcceptTcpClient( ar );

			// Process the connection here. (Add the client to a 
			// server table, read data, etc.)
			Guid clientId = Guid.NewGuid();

			Logger.LogInfo( String.Format( "Client {0} connected", clientId ) );

			m_Clients.AddOrUpdate(clientId.ToString(), client, (k, v) => client );

			OnTcpClinetConnected( new NewTcpClientEventArgs( Guid.NewGuid().ToString(), client ) );
			
		}

		public IEnumerable<string> ClientIds { get { return m_Clients.Keys; } }

		protected virtual void OnTcpClinetConnected( EventArgs e ) {
			EventHandler handler = TCPClinetConnected;
			if ( handler != null ) {
				handler( this, e );
			}
		}

		public class NewTcpClientEventArgs : EventArgs {
			public string Id { get; set; }
			public TcpClient Client { get; set; }

			public NewTcpClientEventArgs(string id, TcpClient client) {
				Id = id;
				Client = client;
			}
		}

		
	}
}