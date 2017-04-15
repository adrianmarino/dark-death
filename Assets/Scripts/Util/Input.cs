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
			return NextMovementVariation (MOUSE_X);
		}

		public static float NextMouseVercalMovementVariation ()
		{
			return NextMovementVariation (MOUSE_Y);
		}

		public static float NextHorizontalMovementVariation ()
		{
			return NextMovementVariation (HORIZONTAL);
		}

		public static float NextVerticalMovementVariation ()
		{
			return NextMovementVariation (VERTICAL);
		}

		public static float NextMovementVariation (string axisName)
		{
			return UnityEngine.Input.GetAxisRaw (axisName);
		}

		public static bool GetJumpButtonDown ()
		{
			return UnityEngine.Input.GetButtonDown (JUMP);
		}

		public static bool GetJumpButtonUp ()
		{
			return UnityEngine.Input.GetButtonUp (JUMP);
		}

		public static bool GetFireButton ()
		{
			return UnityEngine.Input.GetButton (FIRE_1);
		}


		public static bool GetFireButtonUp ()
		{
			return UnityEngine.Input.GetButtonUp (FIRE_1);
		}

		public static bool GetFireButtonDown ()
		{
			return UnityEngine.Input.GetButtonDown (FIRE_1);
		}

		//-----------------------------------------------------------------------------
		// Constants
		//-----------------------------------------------------------------------------

		private const string FIRE_1 = "Fire1";

		private const string JUMP = "Jump";

		private const string VERTICAL = "Vertical";

		private const string HORIZONTAL = "Horizontal";

		private const string MOUSE_X = "Mouse X";

		private const string MOUSE_Y = "Mouse Y";

		//-----------------------------------------------------------------------------
		// Constructors
		//-----------------------------------------------------------------------------

		private Input ()
		{
		}
	}
}
