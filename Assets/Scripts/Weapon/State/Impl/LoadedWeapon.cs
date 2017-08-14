using Fps.Weapon.State;
using UnityEngine;

namespace Fps.Weapon
{
	public class LoadedWeapon : WeaponState
	{
		//-----------------------------------------------------------------------------
		// Public Methods
		//-----------------------------------------------------------------------------

		public override bool Shoot (Transform origin, out RaycastHit target, LayerMask targetMask)
		{
			Debug.Log (weapon + " shoot bullet " + ++shotCounter);
			return weapon.ShootAction (origin, out target, targetMask);
		}

		public override void PlayShootEffect ()
		{
			--remainAmmo;
			if (remainAmmo == 0)
				weapon.GoToUnloadedState ();
			weapon.PlayShootEffectAction ();
		}

		public override void HitTarget (Vector3 position, Vector3 normal)
		{
			weapon.HitTargetAction (position, normal);
		}

		public override void Reload ()
		{
			weapon.Reload ();
		}

		//-----------------------------------------------------------------------------
		// Properties
		//-----------------------------------------------------------------------------

		public override int RemainAmmo {
			get { return remainAmmo; }
		}

		//-----------------------------------------------------------------------------
		// Attributes
		//-----------------------------------------------------------------------------

		protected RechargeableWeapon weapon;

		private int remainAmmo;

		private int shotCounter;

		//-----------------------------------------------------------------------------
		// Constructors
		//-----------------------------------------------------------------------------

		public LoadedWeapon (RechargeableWeapon weapon, int maxAmmo)
		{
			this.weapon = weapon;
			remainAmmo = maxAmmo;
			shotCounter = 0;
			Debug.Log (weapon + " loaded with " + RemainAmmo + " bullets");
		}
	}
}