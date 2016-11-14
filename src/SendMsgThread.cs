using System;
using SwinGameSDK;
using System.Collections.Generic;
using System.Threading;

namespace ChatProgram
{
	public class SendMsgThread
	{
		private String _message;
		private List<Connection> _connectionList;
		private Thread t;

		public SendMsgThread (String aMsg, List<Connection> aConnectionList )
		{
			_message = aMsg;
			_connectionList = aConnectionList;

			t = new Thread (new ThreadStart (SendMessage));
			t.Start ();
		}

		private void SendMessage()
		{
			foreach (Connection con in _connectionList)
				SwinGame.SendUDPMessage (_message, con);
		}
	}
}

