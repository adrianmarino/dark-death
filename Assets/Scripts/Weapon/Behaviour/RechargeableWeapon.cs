using Fps.Weapon;
using UnityEngine;
using Fps.Weapon.State;

namespace Fps.Weapon
{
	public abstract class RechargeableWeapon : BaseWeapon
	{
		//-----------------------------------------------------------------------------
		// Public Methods
		//-----------------------------------------------------------------------------

		public void GoToUnloadedState ()
		{
			State = UnloadState ();
		}

		public void GoToLoadedState ()
		{
			State = InitState ();
		}

		public abstract void PlayReloadEffectAction ();

		//-----------------------------------------------------------------------------
		// Protected Methods
		//-----------------------------------------------------------------------------

		protected override WeaponState InitState ()
		{
			return new LoadedWeapon (this, maxAmmo);
		}

		protected WeaponState UnloadState ()
		{
			return new UnloadedWeapon (this);
		}

		//-----------------------------------------------------------------------------
		// Attributes
		//-----------------------------------------------------------------------------

		[SerializeField]
		protected int maxAmmo = 5;

		//-----------------------------------------------------------------------------
		// Constructors
		//-----------------------------------------------------------------------------

		protected RechargeableWeapon () : base ()
		{
		}
	}
}