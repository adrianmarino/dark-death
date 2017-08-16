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
			State.Reload ();
		}

		public void GoToUnloadedState ()
		{
			State = UnloadState ();
		}

		public void GoToLoadingState ()
		{
			State = LoadingState ();
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

		protected WeaponState LoadingState ()
		{
			return new LoadingWeaponState (this, WeaponReloadAnimation ().Duration);
		}

		protected override WeaponState InitState ()
		{
			return new LoadedWeaponState (this, maxAmmo);
		}

		protected WeaponState UnloadState ()
		{
			return new UnloadedWeaponState (this);
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