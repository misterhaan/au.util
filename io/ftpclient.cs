using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;

namespace au.io {

	public enum FTPTransferType {
		Binary,
		ASCII
	}

	/// <summary>
	/// Client connection to an FTP server
	/// </summary>
	public class FTPClient {

		#region constants
		private const int BLOCKSIZE = 512;
		#endregion  // constants

		#region data members
		private Socket _sock;
		private IPEndPoint _ip;

		private int _rspCode;
		private string _rspMessage;
		#endregion  // data members

		#region constructors
		/// <summary>
		/// Set parameters for FTP connection
		/// </summary>
		/// <param name="hostname">Name of FTP server to connect to</param>
		/// <param name="port">Port of FTP server to connect to</param>
		public FTPClient(string hostname, int port) {
			_sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			_ip = new IPEndPoint(Dns.Resolve(hostname).AddressList[0], port);
		}
		#endregion  // constructor

		#region properties
		/// <summary>
		/// Last response code recieved from the FTP server
		/// </summary>
		public int Code {
			get {return _rspCode;}
		}
		
		/// <summary>
		/// Last message recieved from the FTP server
		/// </summary>
		public string Message {
			get {return _rspMessage;}
		}
		#endregion  // properties

		#region methods
		/// <summary>
		/// Attempts to open the FTP connection
		/// </summary>
		/// <param name="username">Username to log in to FTP server with</param>
		/// <param name="password">Password to log in to FTP server with</param>
		/// <returns>True if the connection is opened</returns>
		public bool Open(string username, string password) {
			_sock.Connect(_ip);
			GetResponse();
			if(_rspCode != 220) {
				Close();
				return false;
			}
			Send(@"USER " + username);
			if(_rspCode != 230 && _rspCode != 331) {
				Bye();
				return false;
			}
			if(_rspCode != 230) {
				Send(@"PASS " + password);
				if(_rspCode != 230 && _rspCode != 202) {
					Bye();
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// Closes the FTP connection
		/// </summary>
		public void Bye() {
			if(_sock != null)
				Send("QUIT");
			Close();
		}

		/// <summary>
		/// Closes the socket used by the FTP connection
		/// </summary>
		private void Close() {
			if(_sock != null)
				_sock.Close();
		}

		/// <summary>
		/// Sends a command to the FTP server
		/// </summary>
		/// <param name="command"></param>
		public void Send(string command) {
			byte[] buffer = Encoding.ASCII.GetBytes((command + "\r\n").ToCharArray());
			_sock.Send(buffer);
			GetResponse();
		}

		/// <summary>
		/// Change working directory on the FTP server
		/// </summary>
		/// <param name="path">Directory to change to</param>
		/// <returns>True if successful</returns>
		public bool CWD(string path) {
			Send("CWD " + path);
			return _rspCode == 250;
		}

    /// <summary>
    /// Sends a file to the FTP server
    /// </summary>
    /// <param name="filename">Full path to the file to send</param>
    /// <returns>True if file is sent</returns>
    public bool Download(string filename, FTPTransferType type) {
      if(type.Equals(FTPTransferType.ASCII))
        Send(@"TYPE A");
      else
        Send(@"TYPE I");
      if(_rspCode != 200)
        return false;
      Socket data = OpenDataSocket();
      if(data == null) {
        _rspCode = -1;
        _rspMessage = @"Unable to open socket for file transfer";
        return false;
      }
      Send(@"STOR " + Path.GetFileName(filename));
      if(_rspCode != 125 && _rspCode != 150)
        return false;
      try {
        FileStream fs = new FileStream(filename, FileMode.Open);
        byte[] buffer = new byte[BLOCKSIZE];
        int bytes;
        while((bytes = fs.Read(buffer, 0, BLOCKSIZE)) > 0) {
          data.Send(buffer, bytes, 0);
        }
        fs.Close();
        data.Close();
        GetResponse();
        if(_rspCode != 226 && _rspCode != 250)
          return false;
        return true;
      } catch {
        _rspCode = -1;
        _rspMessage = @"Unable to read file for upload";
        return false;
      }
    }
    
    /// <summary>
    /// Sends a string to the FTP server as a file
    /// </summary>
    /// <param name="filename">File name to create on FTP server (no path)</param>
    /// <param name="contents">Contents of the file to create</param>
    /// <returns>True if file is sent</returns>
    public bool DownloadText(string filename, string contents) {
      Send(@"TYPE A");
      if(_rspCode != 200)
        return false;
      Socket data = OpenDataSocket();
      if(data == null) {
        _rspCode = -1;
        _rspMessage = @"Unable to open socket for file transfer";
        return false;
      }
      Send(@"STOR " + filename);
      if(_rspCode != 125 && _rspCode != 150)
        return false;
      byte[] buffer;
      for(int pos = 0; pos < contents.Length; pos += BLOCKSIZE) {
        if(contents.Length - pos < BLOCKSIZE)
          buffer = Encoding.ASCII.GetBytes(contents.ToCharArray(), pos, contents.Length - pos);
        else
          buffer = Encoding.ASCII.GetBytes(contents.ToCharArray(), pos, BLOCKSIZE);
        data.Send(buffer, buffer.Length, 0);
      }
      data.Close();
      GetResponse();
      if(_rspCode != 226 && _rspCode != 250)
        return false;
      return true;
    }
    #endregion  // methods

		#region private methods
		/// <summary>
		/// Read the response from the FTP server
		/// </summary>
		private void GetResponse() 
    {
			byte[] buffer = new byte[BLOCKSIZE];
			int bytes;
			string msg = @"";
			do {
				bytes = _sock.Receive(buffer, BLOCKSIZE, 0);
				msg += Encoding.ASCII.GetString(buffer, 0, bytes);
			} while(bytes > BLOCKSIZE);
			string[] msgAr = msg.Replace("\r\n", "\n").Split('\n');
			if(msgAr.Length > 2)
				msg = msgAr[msgAr.Length - 2];
			else
				msg = msgAr[0];
			_rspCode = int.Parse(msg.Substring(0, 3));
			_rspMessage = msg.Substring(4);
		}

		/// <summary>
		/// Open a data socket for transferring
		/// </summary>
		/// <returns></returns>
		private Socket OpenDataSocket() {
			Send(@"PASV");
			if(_rspCode != 227)
				return null;
			try {
				string[] pasvdata = _rspMessage.Split('(')[1].Split(')')[0].Split(',');
				string ipAddress = pasvdata[0] + '.' + pasvdata[1] + '.' + pasvdata[2] + '.' + pasvdata[3];
				int port = (int.Parse(pasvdata[4]) << 8) + int.Parse(pasvdata[5]);
				IPEndPoint ip = new IPEndPoint(Dns.Resolve(ipAddress).AddressList[0], port);
				Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				try {
					sock.Connect(ip);
					return sock;
				} catch {
					return null;
				}
			} catch {
				return null;  // response to PASV not as expected
			}
		}
		#endregion  // private methods
	}
}
