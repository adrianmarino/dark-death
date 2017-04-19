using UnityEngine;
using UnityEngine.Networking;

namespace Fps
{
	public class Weapon : NetworkBehaviour
	{
		//-----------------------------------------------------------------------------
		// Public Methods
		//-----------------------------------------------------------------------------
	
		public void Hit (Vector3 position, Vector3 normal)
		{
			GameObject hitEffect = Instantiate (
				                       HitEffect,
				                       position,
				                       Quaternion.LookRotation (normal)
			                       );
			Destroy (hitEffect, 2f);
		}

		public void PlayShootEffects ()
		{
			MuzzleFlash.Play ();
			GetComponent<AudioSource> ().Play ();
		}

		//-----------------------------------------------------------------------------
		// Properties
		//-----------------------------------------------------------------------------

		public ParticleSystem MuzzleFlash {
			get { return muzzleFlash; }
		}

		public GameObject HitEffect {
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
		private GameObject hitEffect;

		[SerializeField]
		private string _name = "HeavyBlaster";

		[SerializeField]
		private float damage = 25f;

		[SerializeField]
		private float range = 100f;

		[SerializeField]
		private float fireRate = 2f;
	}
}

