using UnityEngine;
using Fps.Weapon.Animation;

namespace Fps.Weapon
{
	[RequireComponent (typeof(WeaponRecoilAnimation))]
	[RequireComponent (typeof(WeaponSwayAnimation))]
	[RequireComponent (typeof(WeaponReloadAnimation))]
	public class Rifle : RechargeableWeapon
	{
		//-----------------------------------------------------------------------------
		// Public Methods
		//-----------------------------------------------------------------------------

		public override void HitTargetAction (Vector3 position, Vector3 normal)
		{
			GameObject _hitEffect = Instantiate (
				                        HitEffect,
				                        position,
				                        Quaternion.LookRotation (normal)
			                        );
			Destroy (_hitEffect, 2f);
		}

		public override void PlayShootEffectAction ()
		{
			muzzleFlash.Play ();
			ShootSound.Play ();
			shootSmoke.Play ();
			WeaponRecoilAnimation ().Play ();
		}

		public override void PlayReloadEffectAction ()
		{
			WeaponReloadAnimation ().Play ();
			ReloadSound.Play ();
		}

		//-----------------------------------------------------------------------------
		// Properties
		//-----------------------------------------------------------------------------

		AudioSource ShootSound {
			get { return GetComponents<AudioSource> () [0]; }
		}

		AudioSource ReloadSound {
			get { return GetComponents<AudioSource> () [1]; }
		}

		WeaponRecoilAnimation WeaponRecoilAnimation ()
		{
			return GetComponent<WeaponRecoilAnimation> ();
		}

		WeaponReloadAnimation WeaponReloadAnimation ()
		{
			return GetComponent<WeaponReloadAnimation> ();
		}

		GameObject HitEffect {
			get { return hitEffect; }
		}

		//-----------------------------------------------------------------------------
		// Attributes
		//-----------------------------------------------------------------------------

		[SerializeField]
		private ParticleSystem muzzleFlash;

		[SerializeField]
		private ParticleSystem shootSmoke;

		[SerializeField]
		private GameObject hitEffect;

		//-----------------------------------------------------------------------------
		// Constructors
		//-----------------------------------------------------------------------------

		private Rifle () : base ()
		{
		}
	}
}
