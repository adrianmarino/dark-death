using AClockworkBerry;

namespace Util
{
	public class Console
	{
		public static void Clean ()
		{
			for (int i = 0; i < 40; i++)
				ScreenLogger.print ("");
		}

		//-----------------------------------------------------------------------------
		// Constructors
		//-----------------------------------------------------------------------------

		private Console ()
		{
		}
	}
}
