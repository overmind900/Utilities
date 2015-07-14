using System;
using System.Diagnostics;
namespace Utilities.Logging {
	public static class Logger {
		
		/// <summary>
		/// Logging level to write to logs
		/// </summary>
		public enum LogLevel {
			Debug = 0,
			Info = 1,
			Warn = 2,
			Error = 3
		}

		private static LogLevel m_LoggingLevel = LogLevel.Info;

		public static LogLevel LoggingLevel {
			get { return m_LoggingLevel; }
			set { m_LoggingLevel = value; }
		}


		/// <summary>
		/// logger implementation. Console as default logger
		/// </summary>
		private static ILogger m_LogHangler = new ConsoleLogger();

		/// <summary>
		/// Initializes a new static instance of the Logger class
		/// </summary>
		static Logger() { }

		/// <summary>
		/// Setter for logger implementation
		/// </summary>
		public static ILogger LogHandler { set { m_LogHangler = value; } }

		/// <summary>
		/// Log Debug message (not logged by default) set log level to debug to log
		/// </summary>
		/// <param name="message">Pre-formatted message</param>
		public static void LogDebug( string message ) {
			if( m_LoggingLevel <= LogLevel.Debug ) {
				m_LogHangler.LogMessage( string.Format( "DEBUG {0}: {1}", DateTime.UtcNow, message ) );	
			}
		}

		/// <summary>
		/// Log Info message
		/// </summary>
		/// <param name="message">Pre-formatted message</param>
		public static void LogInfo( string message ) {
			if ( m_LoggingLevel <= LogLevel.Info ) {
				m_LogHangler.LogMessage( string.Format( "INFO {0}: {1}", DateTime.UtcNow, message ) ); 
			}
		}

		/// <summary>
		/// Log Warning message. No different then info except shows in log with WARN instead of INFO
		/// </summary>
		/// <param name="message">Pre-formatted message</param>
		public static void LogWarning( string message ) {
			if ( m_LoggingLevel <= LogLevel.Warn ) {
				m_LogHangler.LogMessage( string.Format( "WARN {0}: {1}", DateTime.UtcNow, message ) ); 
			}
		}

		/// <summary>
		/// Log Error Message then throw exception
		/// </summary>
		/// <param name="message">Pre-formatted message</param>
		public static void LogError( string message ) {
			if ( m_LoggingLevel <= LogLevel.Error ) {
				StackTrace stack = new StackTrace( true );
				m_LogHangler.LogMessage( string.Format( "ERROR {0}: {1} \n {2}", DateTime.UtcNow, message, stack ) ); 
			}
			throw new Exception( message );
		}

		/// <summary>
		/// Log Error message and re-throw exception
		/// </summary>
		/// <param name="message">Pre-formatted message</param>
		/// <param name="e">Exception to re-throw</param>
		public static void LogError( string message, Exception e ) {
			if ( m_LoggingLevel <= LogLevel.Error ) {
				m_LogHangler.LogMessage( string.Format( "ERROR {0}: {1} \n {2}", DateTime.UtcNow, message, e ) ); 
			}
			throw new Exception( message, e);
		}


		 
	}
}