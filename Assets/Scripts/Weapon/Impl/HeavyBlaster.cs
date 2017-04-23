using UnityEngine;
using Fps.Weapon.Animation;

namespace Fps.Weapon
{
	[RequireComponent (typeof(WeaponRecoil))]
	[RequireComponent (typeof(WeaponSway))]
	public class HeavyBlaster : MonoBehaviour, IWeapon
	{
		//-----------------------------------------------------------------------------
		// Public Methods
		//-----------------------------------------------------------------------------

		public bool Shoot (Transform origin, out RaycastHit target, LayerMask targetMask)
		{
			return Physics.Raycast (
				origin.position, 
				origin.forward, 
				out target, 
				targetMask
			);
		}

		public void HitTarget (Vector3 position, Vector3 normal)
		{
			GameObject hitEffect = Instantiate (
				                       HitEffect,
				                       position,
				                       Quaternion.LookRotation (normal)
			                       );
			Destroy (hitEffect, 2f);
		}

		public void PlayShootEffect ()
		{
			muzzleFlash.Play ();
			ShootSound.Play ();
			shootSmoke.Play ();
			WeaponRecoilAnimation ().Play ();
		}

		public override string ToString ()
		{
			return Name;
		}

		public void Hide ()
		{
			model.SetActive (false);
		}

		public void Show ()
		{
			model.SetActive (true);
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

		public string Name {
			get { return _name; }
		}

		public float Damage {
			get { return damage; }
		}

		public float Range {
			get { return range; }
		}

		public float FireRate {
			get { return fireRate; }
		}

		public GameObject GameObject {
			get { return this.gameObject; }
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

		[SerializeField]
		private string _name = "HeavyBlaster";

		[SerializeField]
		private float damage = 25f;

		[SerializeField]
		private float range = 100f;

		[SerializeField]
		private float fireRate = 2f;

		[SerializeField]
		private GameObject model;
	}
}