/*
 Revision History:
   2005-08-23 0.0.0 [Jay] created 
*/
using System;

namespace au.io {
  /// <summary>
  /// Type of message (used to specify what kind of message is being reported)
  /// </summary>
	public enum MessageType {
		Information,
		Warning,
		Error
	}

	/// <summary>
	/// Abstract reporter class allows other classes to report messages using a specific interface
	/// </summary>
	public abstract class Reporter {

		/// <summary>
		/// Reports a message.
		/// </summary>
		/// <param name="title">MessageBox caption or beginning of event log message</param>
		/// <param name="message">The actual message, user-friendly</param>
		/// <param name="debugDetail">Details to display if in debug mode</param>
		/// <param name="typ">Type of message being reported</param>
		public abstract void Report(string title, string message, string debugDetail, MessageType typ);

		/// <summary>
		/// Reports a message.
		/// </summary>
		/// <param name="title">MessageBox caption or beginning of event log message</param>
		/// <param name="message">The actual message, user-friendly</param>
		/// <param name="typ">Type of message being reported</param>
		public void Report(string title, string message, MessageType typ) {
			Report(title, message, @"", typ);
		}

		/// <summary>
		/// Reports a message.
		/// </summary>
		/// <param name="title">MessageBox caption or beginning of event log message</param>
		/// <param name="message">The actual message, user-friendly</param>
		public void Report(string title, string message) {
			Report(title, message, @"", MessageType.Error);
		}
	}
}
