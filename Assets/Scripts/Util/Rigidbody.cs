using UnityEngine;
using UnityStandardAssets.Utility;

namespace Util
{
	public class Rigidbody
	{
		public static void Move (UnityEngine.Rigidbody rigidbody, Vector3 velocity)
		{
			if (velocity == Vector3.zero)
				return;
			rigidbody.MovePosition (rigidbody.position + velocity * Time.fixedDeltaTime);
		}

		public static void Rotate (UnityEngine.Rigidbody rigidbody, Vector3 rotation)
		{
			if (rotation == Vector3.zero)
				return;
			rigidbody.MoveRotation (rigidbody.rotation * Quaternion.Euler (rotation));
		}

		//-----------------------------------------------------------------------------
		// Constructors
		//-----------------------------------------------------------------------------

		private Rigidbody ()
		{
		}
	}
}
