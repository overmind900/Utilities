using System;
namespace Utilities.Logging {
	/// <summary>
	/// Logs to the console
	/// </summary>
	public class ConsoleLogger : ILogger{

		/// <summary>
		///   Writes the specified string value, followed by the current line terminator, to the Console.
		/// </summary>
		/// <param name="message">Pre-formatted message</param>
		public void LogMessage( string message ) {
			Console.WriteLine( message );
		}
	}
}