using UnityEngine;

namespace Util
{
	public class GUI
	{
		public static void TextWindow (string title, string content, Rect rect)
		{
			if (content.Length == 0)
				return;

			UnityEngine.GUI.Window (
				0, 
				rect,
				(windowID) => UnityEngine.GUI.TextArea (ContentRectFrom (rect), content), 
				title
			);
		}

		//-----------------------------------------------------------------------------
		// Private Methods
		//-----------------------------------------------------------------------------

		static Rect ContentRectFrom (Rect windowRect)
		{
			return new Rect (5, 18, windowRect.width - 10, windowRect.height - 23);
		}
	}
}