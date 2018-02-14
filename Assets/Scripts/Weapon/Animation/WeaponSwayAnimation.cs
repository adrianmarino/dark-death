using UnityEngine;

namespace Fps.Weapon.Animation
{
	public class WeaponSwayAnimation : MonoBehaviour
	{
		//-----------------------------------------------------------------------------
		// Event Methods
		//-----------------------------------------------------------------------------

		void Update ()
		{
			if (Pause) return;

			transform.localRotation = Quaternion.Slerp (
				transform.localRotation,
				MousePoint (),
				speed * Time.deltaTime
			);
		}

		//-----------------------------------------------------------------------------
		// Private Methods
		//-----------------------------------------------------------------------------

		Quaternion MousePoint ()
		{
			return Quaternion.Euler (
				-Util.Input.MouseVercalMovementDelta (), 
				Util.Input.MouseHorizontalMovementDelta (), 
				0
			);
		}

		public bool Pause { get; set; }

		//-----------------------------------------------------------------------------
		// Attributes
		//-----------------------------------------------------------------------------

		private float mouseX, mouseY;

		private Quaternion mousePoint;

		[SerializeField] private float speed = 1.5f;

		[SerializeField] private bool pause;
	}
}

