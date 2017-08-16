using UnityEngine;
using Fps.Weapon.State;

namespace Fps.Weapon
{
	public class UnloadedWeaponState : WeaponState
	{
		public override bool Shoot (Transform origin, out RaycastHit target, LayerMask targetMask)
		{
			Debug.Log ("Try shoot " + weapon + " without bullets");
			return base.Shoot (origin, out target, targetMask);
		}

		public override void Reload ()
		{
			weapon.GoToLoadingState ();
		}

		//-----------------------------------------------------------------------------
		// Attributes
		//-----------------------------------------------------------------------------

		protected RechargeableWeapon weapon;

		//-----------------------------------------------------------------------------
		// Constructors
		//-----------------------------------------------------------------------------

		public UnloadedWeaponState (RechargeableWeapon weapon)
		{
			this.weapon = weapon;
		}
	}
}