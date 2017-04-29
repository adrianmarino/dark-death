using UnityEngine;
using Fps.Weapon.State;

namespace Fps.Weapon
{
	public class UnloadedWeapon : WeaponState
	{
		public override bool Shoot (Transform origin, out RaycastHit target, LayerMask targetMask)
		{
			Debug.Log ("Try shoot " + weapon + " without bullets");
			return base.Shoot (origin, out target, targetMask);
		}

		public override void Reload ()
		{
			weapon.GoToLoadedState ();
		}

		//-----------------------------------------------------------------------------
		// Attributes
		//-----------------------------------------------------------------------------

		protected RechargeableWeapon weapon;

		//-----------------------------------------------------------------------------
		// Constructors
		//-----------------------------------------------------------------------------

		public UnloadedWeapon (RechargeableWeapon weapon)
		{
			this.weapon = weapon;
		}
	}
}