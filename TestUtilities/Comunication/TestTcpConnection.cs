using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Utilities.Comunication;
namespace TestUtilities.Comunication {
	[TestFixture]
	public class TestTcpConnection {
		TcpConnection A;
		TcpConnection B;
		TcpListener Listener;

		[SetUp]
		public void Setup() {
			Task s = Connect();
			s.Wait();
		}

		[TearDown]
		public void TearDown() {
			A.Close();
			B.Close();
			Listener.Stop();
		}

		private async Task Connect() {
			Listener = new TcpListener( IPAddress.Loopback, 666 );
			Listener.Start();
			Task<TcpClient> getClientA = Listener.AcceptTcpClientAsync();
			B = new TcpConnection( IPAddress.Loopback.ToString(), 666 );
			A = new TcpConnection( await getClientA );
		}

		[Test]
		public void IsConnected() {
			Assert.True( A.Connected );
			Assert.IsNotNullOrEmpty( A.Id );
			Assert.True( B.Connected );
			Assert.IsNotNullOrEmpty( B.Id );
		}

		[Test, Repeat(100)]
		public void SendString() {
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
			var random = new Random(DateTime.UtcNow.Millisecond);
			var result = new string(
				Enumerable.Repeat( chars, random.Next( 2000, 50000 ) ) //random length between 2000(receive buffer is 1000) and 50000
						  .Select( s => s[random.Next( s.Length )] )
						  .ToArray() );
			A.Send( Encoding.UTF8.GetBytes( result ) );
			byte[] recieved = B.Recieve();
			string accual = Encoding.UTF8.GetString( recieved );
			Assert.AreEqual( result, accual );
		}


		 
	}
}