using System;
using SwinGameSDK;
using System.Collections.Generic;
using System.Threading;

namespace ChatProgram
{
	public class ReceiveMsgThread
	{
		private Thread th;
		private Connection _connection;
		public ReceiveMsgThread (Connection aConnection)
		{
			_connection = aConnection;

			th = new Thread (new ThreadStart (Listen));
			th.Start (); 
		}

		private void Listen()
		{
			while (true) 
			{
				if(SwinGame.UDPMessageReceived ())
				{
						String receivedMsg = SwinGame.ReadLastMessage (_connection);

						if (receivedMsg != null && receivedMsg != "") 
							lock (ProgramHandler.MsgList)
								ProgramHandler.MsgList.Add (receivedMsg);
					
				}
			}
		}
	}
}

