using UnityEngine;

namespace Util
{
	public class Rigidbody
	{
		public static void AddForce (UnityEngine.Rigidbody rigidbody, Vector3 force, ForceMode forceMode)
		{
			if (force == Vector3.zero)
				return;
			rigidbody.AddForce (force * Time.fixedDeltaTime, forceMode);
		}

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
