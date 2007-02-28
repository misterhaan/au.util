/*
 Revision History:
   2005-08-23 0.0.0 [Jay] created 
*/
using System;
using System.Windows.Forms;

namespace au.io {
	/// <summary>
	/// Reports messages using a MessageBox (for user mode applications).
	/// </summary>
	public class MessageBoxReporter : Reporter {

		private IWin32Window _owner;
		private string _appName;

		/// <summary>
		/// Creates a new MessageBoxReporter.
		/// </summary>
		/// <param name="owner">The window that MessageBoxes should belong to.</param>
		public MessageBoxReporter(IWin32Window owner, string appName) {
			_owner = owner;
			_appName = appName;
		}

		/// <summary>
		/// Reports a message to the user using a MessageBox
		/// </summary>
		/// <param name="title">MessageBox caption</param>
		/// <param name="message">The actual message, user-friendly</param>
		/// <param name="debugDetail">Details to display if in debug mode</param>
		/// <param name="typ">Type of message being reported</param>
		public override void Report(string title, string message, string debugDetail, MessageType typ) {
			MessageBoxIcon icon;
			switch(typ) {
				case MessageType.Information: icon = MessageBoxIcon.Information; break;
				case MessageType.Warning: icon = MessageBoxIcon.Warning; break;
				default: icon = MessageBoxIcon.Error; break;
			}
			#if DEBUG
			if(debugDetail != null && debugDetail.Length > 0)
				message += "\n\nDetails:\n" + debugDetail;
			#endif
			MessageBox.Show(_owner, message, title + @" - " + _appName, MessageBoxButtons.OK, icon);
		}
	}
}
