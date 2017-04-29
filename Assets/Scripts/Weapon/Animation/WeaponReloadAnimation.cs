using UnityEngine;
using DG.Tweening;

namespace Fps.Weapon.Animation
{
	public class WeaponReloadAnimation : MonoBehaviour
	{
		//-----------------------------------------------------------------------------
		// Public Methods
		//-----------------------------------------------------------------------------

		public void Play ()
		{
			// Stop and complete any animation we had on the weapon.
			transform.DOKill (true);

			// Use punch tweens to animate the weapon to slightly randomized values.
			transform.DOPunchPosition (
				translation * Random.Range (1.3f, 1.7f), 
				duration,
				vibrate, 
				elasticity
			).SetRelative ();

			transform.DOPunchRotation (
				rotation * Random.Range (1.3f, 1.7f), 
				duration, 
				vibrate, 
				elasticity
			).SetRelative ();
		}

		//-----------------------------------------------------------------------------
		// Attributes
		//-----------------------------------------------------------------------------

		[SerializeField]
		private Vector3 translation = new Vector3 (-0.3f, -0.3f, 0.2f);

		[SerializeField]
		private Vector3 rotation = new Vector3 (20, -30f, 0);

		[SerializeField]
		private float duration = 1.6f;

		[SerializeField]
		private int vibrate = 0;

		[SerializeField]
		private float elasticity = 0;
	}
}

