using System;
using System.Diagnostics;
namespace Utilities.Logging {
	public static class Logger {

		private static ILogger m_LogHangler;

		static Logger() {
			//choose console as default logger
			m_LogHangler = new ConsoleLogger();
		}

		public static ILogger LogHandler { set { m_LogHangler = value; } }

		public static void LogInfo( string message ) {
			m_LogHangler.LogMessage( string.Format( "INFO {0}: {1}", DateTime.UtcNow, message ) );
		}

		public static void LogWarning( string message ) {
			m_LogHangler.LogMessage( string.Format( "WARN {0}: {1}", DateTime.UtcNow, message ) );
		}

		public static void LogError( string message ) {
			StackTrace stack = new StackTrace( true );
			m_LogHangler.LogMessage( string.Format( "ERROR {0}: {1} \n {2}", DateTime.UtcNow, message, stack ) );
			throw new Exception( message );
		}

		public static void LogError( string message, Exception e ) {
			m_LogHangler.LogMessage( string.Format( "ERROR {0}: {1} \n {2}", DateTime.UtcNow, message, e ) );
			throw new Exception( message, e);
		}


		 
	}
}