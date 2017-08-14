using Fps.Weapon;
using UnityEngine;
using Fps.Weapon.State;
using Fps.Weapon.Animation;

namespace Fps.Weapon
{
	[RequireComponent (typeof(WeaponReloadAnimation))]
	public class RechargeableWeapon : BaseWeapon
	{
		//-----------------------------------------------------------------------------
		// Public Methods
		//-----------------------------------------------------------------------------

		public override void Reload ()
		{
			this.PlayReloadEffectAction ();
			this.GoToLoadedState ();
		}

		public void GoToUnloadedState ()
		{
			State = UnloadState ();
		}

		public void GoToLoadedState ()
		{
			State = InitState ();
		}

		public void PlayReloadEffectAction ()
		{
			WeaponReloadAnimation ().Play ();
			ReloadSound.Play ();
		}

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
		// Properties
		//-----------------------------------------------------------------------------

		AudioSource ReloadSound {
			get { return GetComponents<AudioSource> () [1]; }
		}

		WeaponReloadAnimation WeaponReloadAnimation ()
		{
			return GetComponent<WeaponReloadAnimation> ();
		}

		//-----------------------------------------------------------------------------
		// Attributes
		//-----------------------------------------------------------------------------

		[SerializeField]
		protected int maxAmmo = 10;

		//-----------------------------------------------------------------------------
		// Constructors
		//-----------------------------------------------------------------------------

		protected RechargeableWeapon () : base ()
		{
		}
	}
}