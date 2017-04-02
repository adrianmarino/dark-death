using UnityEngine;

namespace Util
{
	public class Input
	{
		public static void HideCursor (bool value)
		{
			if (value)
				HideCursor ();
			else
				ShowCursor ();
		}

		public static void HideCursor ()
		{
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
		}


		public static void ShowCursor ()
		{
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
		}

		public static Vector2 NextKeyboardHorVerMovementVariation ()
		{
			return new Vector2 (
				NextHorizontalMovementVariation (), 
				NextVerticalMovementVariation ()
			);
		}

		public static Vector2 NextMouseHorVerMovementVariation ()
		{
			return new Vector2 (
				NextMouseHorizontalMovementVariation (), 
				NextMouseVercalMovementVariation ()
			);
		}

		public static float NextMouseHorizontalMovementVariation ()
		{
			return NextMovementVariation ("Mouse X");
		}

		public static float NextMouseVercalMovementVariation ()
		{
			return NextMovementVariation ("Mouse Y");
		}

		public static float NextHorizontalMovementVariation ()
		{
			return NextMovementVariation ("Horizontal");
		}

		public static float NextVerticalMovementVariation ()
		{
			return NextMovementVariation ("Vertical");
		}

		public static float NextMovementVariation (string axisName)
		{
			return UnityEngine.Input.GetAxisRaw (axisName);
		}

		public static bool GetJumpButton ()
		{
			return UnityEngine.Input.GetButton ("Jump");
		}

		//-----------------------------------------------------------------------------
		// Constructors
		//-----------------------------------------------------------------------------

		private Input ()
		{
		}
	}
}
