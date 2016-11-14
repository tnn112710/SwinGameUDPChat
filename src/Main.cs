//Main.cs

using System;
using SwinGameSDK;

namespace ChatProgram
{
	/// <summary>
	/// Main Entry
	/// </summary>
	public class GameMain
	{
		public static void Main ()
		{
			ProgramHandler handler = new ProgramHandler ();

			SwinGame.OpenGraphicsWindow ("Chat Program", 800, 600);

			while (false == SwinGame.WindowCloseRequested ()) {

				SwinGame.ProcessEvents ();
				SwinGame.ClearScreen (Color.White);

				handler.GetUserInput ();
				handler.PrintMessages ();

				SwinGame.RefreshScreen (60);
			}
		}
	}
}