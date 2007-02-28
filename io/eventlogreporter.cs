using System;
using System.Diagnostics;

namespace au.io {
	/// <summary>
	/// Reports a message to the windows event log (for non-user applications).
	/// </summary>
	public class EventLogReporter : Reporter {
		private string _source;

		/// <summary>
		/// Creates a new EventLogReporter.
		/// </summary>
		/// <param name="eventSource">Text to show in the source column of the event viewer.</param>
		public EventLogReporter(string eventSource) {
			_source = eventSource;
			if(!EventLog.SourceExists(_source))
				EventLog.CreateEventSource(_source, "Application");
		}

		/// <summary>
		/// Reports a message to the windows event log.
		/// </summary>
		/// <param name="title">Beginning of event log message</param>
		/// <param name="message">The actual message, user-friendly</param>
		/// <param name="debugDetail">Details to display at end of message</param>
		/// <param name="typ">Type of message being reported</param>
		public override void Report(string title, string message, string debugDetail, MessageType typ) {
			EventLogEntryType et;
			switch(typ) {
				case MessageType.Information: et = EventLogEntryType.Information; break;
				case MessageType.Warning: et = EventLogEntryType.Warning; break;
				default: et = EventLogEntryType.Error; break;
			}
			message = title + "\n\n" + message;
			if(debugDetail != null && debugDetail.Length > 0)
				message += "\n\nDetails:\n" + debugDetail;
			EventLog.WriteEntry(_source, message, et);
		}
	}
}
