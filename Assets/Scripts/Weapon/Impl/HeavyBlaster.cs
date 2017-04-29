﻿using UnityEngine;
using Fps.Weapon.Animation;

namespace Fps.Weapon
{
	[RequireComponent (typeof(WeaponRecoil))]
	[RequireComponent (typeof(WeaponSway))]
	public class HeavyBlaster : RechargeableWeapon
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
			WeaponRecoilAnimation ().Play ();
		}

		//-----------------------------------------------------------------------------
		// Properties
		//-----------------------------------------------------------------------------

		AudioSource ShootSound {
			get { return GetComponent<AudioSource> (); }
		}

		WeaponRecoil WeaponRecoilAnimation ()
		{
			return GetComponent<WeaponRecoil> ();
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

		private HeavyBlaster () : base ()
		{
		}
	}
}