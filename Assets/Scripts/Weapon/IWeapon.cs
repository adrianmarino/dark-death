using UnityEngine;

namespace Fps.Weapon
{
	public interface IWeapon
	{
		//-----------------------------------------------------------------------------
		// Public Methods
		//-----------------------------------------------------------------------------

		bool Shoot (Transform origin, out RaycastHit target, LayerMask targetMask);

		void HitTarget (Vector3 position, Vector3 normal);

		void PlayShootEffect ();

		void Hide ();

		void Show ();

		//-----------------------------------------------------------------------------
		// Properties
		//-----------------------------------------------------------------------------

		GameObject GameObject {
			get;
		}

		float Damage {
			get;
		}

		float Range {
			get;
		}

		float FireRate {
			get;
		}
	}
}