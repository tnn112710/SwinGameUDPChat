using System;
using System.Collections.Generic;
using System.Net;
using SwinGameSDK;

namespace ChatProgram
{
	public class ProgramHandler
	{
		private static List<String> _msgList;
		private static List<Connection> _chatConnection;

		public ProgramHandler ()
		{
			_msgList = new List<string> ();
			_chatConnection = new List<Connection> ();
		}


		public void GetUserInput()
		{
			if (!SwinGame.ReadingText ())
			{
				String input = SwinGame.EndReadingText ();

				if (input != null && input != "") 
				{
					if (ValidateIP (input)) {
						Connection newConnection = SwinGame.CreateUDPConnection (input, 4001, 4001);

						_chatConnection.Add (newConnection);
						ReceiveMsgThread receiveThread = new ReceiveMsgThread (newConnection);
						Console.WriteLine ("Messages will be sent to " + input);
					}
					else 
					{
						_msgList.Add ("You: " + input);
						String msgToSend = SwinGame.MyIP () + ": " + input;
						SendMessage (msgToSend);
					}
				}

				SwinGame.StartReadingText (Color.Red, 40, SwinGame.LoadFont ("Arial", 30), 10, 550);
			}
		}

		public void PrintMessages ()
		{
			lock (_msgList) 
			{
				if (_msgList.Count > 16)
					_msgList.RemoveAt (0);


				if (_msgList.Count != 0) 
				{
					int ySpace = 10;
					foreach (String msg in _msgList) 
					{
						if (msg.Substring (0, 3) != "You")
							SwinGame.DrawText (msg, Color.Blue, SwinGame.LoadFont ("Arial", 20), 10, ySpace);
						else
							SwinGame.DrawText (msg, Color.Green, SwinGame.LoadFont ("Arial", 20), 10, ySpace);
						ySpace += 30;
					}
				}
			}
		}

		private void SendMessage (String aMsg)
		{
			if (_chatConnection.Count != 0)
			{
				SendMsgThread sendMsg = new SendMsgThread (aMsg, _chatConnection);
			}
		}

		private bool ValidateIP (String aInput)
		{
			if (aInput.Contains (".") || aInput.Contains (":")) {
				IPAddress address;
				if (IPAddress.TryParse (aInput, out address)) {
					switch (address.AddressFamily) {
					case System.Net.Sockets.AddressFamily.InterNetwork:
						return true;
					case System.Net.Sockets.AddressFamily.InterNetworkV6:
						return true;
					}
				}
			}
			return false;
		}

		public static List<string> MsgList 
		{
			get {
				return _msgList;
			}

			set {
				_msgList = value;
			}
		}

		public static List<Connection> ChatConnection {
			get {
				return _chatConnection;
			}

			set {
				_chatConnection = value;
			}
		}
	}
}

