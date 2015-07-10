using System;
using System.Diagnostics;
namespace Utilities.Logging {
	public static class Logger {

		/// <summary>
		/// logger implementation
		/// </summary>
		private static ILogger m_LogHangler;

		/// <summary>
		/// Static ctor uses ConsoleLogger as default
		/// </summary>
		static Logger() {
			//choose console as default logger
			m_LogHangler = new ConsoleLogger();
		}

		/// <summary>
		/// Setter for logger implementation
		/// </summary>
		public static ILogger LogHandler { set { m_LogHangler = value; } }

		/// <summary>
		/// Log Info message
		/// </summary>
		/// <param name="message">Pre-formatted message</param>
		public static void LogInfo( string message ) {
			m_LogHangler.LogMessage( string.Format( "INFO {0}: {1}", DateTime.UtcNow, message ) );
		}

		/// <summary>
		/// Lgo Warning message. No different then info except shows in log with WARN instead of INFO
		/// </summary>
		/// <param name="message">Pre-formatted message</param>
		public static void LogWarning( string message ) {
			m_LogHangler.LogMessage( string.Format( "WARN {0}: {1}", DateTime.UtcNow, message ) );
		}

		/// <summary>
		/// Log Error Message then throw exception
		/// </summary>
		/// <param name="message">Pre-formatted message</param>
		public static void LogError( string message ) {
			StackTrace stack = new StackTrace( true );
			m_LogHangler.LogMessage( string.Format( "ERROR {0}: {1} \n {2}", DateTime.UtcNow, message, stack ) );
			throw new Exception( message );
		}

		/// <summary>
		/// Log Error message and re-throw exception
		/// </summary>
		/// <param name="message">Pre-formatted message</param>
		/// <param name="e">Exception to re-throw</param>
		public static void LogError( string message, Exception e ) {
			m_LogHangler.LogMessage( string.Format( "ERROR {0}: {1} \n {2}", DateTime.UtcNow, message, e ) );
			throw new Exception( message, e);
		}


		 
	}
}