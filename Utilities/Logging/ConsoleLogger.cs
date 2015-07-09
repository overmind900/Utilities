using System;
namespace Utilities.Logging {
	public class ConsoleLogger : ILogger{

		public void LogMessage( string message ) {
			System.Console.WriteLine( message );
		}
	}
}