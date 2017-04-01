using UnityEngine;
using System;

namespace Util
{
	public class Input
	{
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

		//-----------------------------------------------------------------------------
		// Constructors
		//-----------------------------------------------------------------------------

		private Input ()
		{
		}
	}
}
