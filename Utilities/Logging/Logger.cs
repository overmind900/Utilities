using System;
using System.Diagnostics;
namespace Utilities.Logging {
	public static class Logger {

		private static ILogger log;

		static Logger() {
			//choose console as default logger
			log = new ConsoleLogger();
		}

		public static void LogInfo( string message ) {
			log.LogMessage( string.Format( "INFO {0}: {1}", DateTime.UtcNow, message ) );
		}

		public static void LogWarning( string message ) {
			log.LogMessage( string.Format( "WARN {0}: {1}", DateTime.UtcNow, message ) );
		}

		public static void LogError( string message ) {
			StackTrace stack = new StackTrace( true );
			log.LogMessage( string.Format( "ERROR {0}: {1} {3}", DateTime.UtcNow, message, stack ) );
			throw new Exception( message );
		}


		 
	}
}