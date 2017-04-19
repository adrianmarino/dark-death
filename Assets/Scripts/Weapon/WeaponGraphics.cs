using UnityEngine;

namespace Fps
{
	public class WeaponGraphics : MonoBehaviour
	{
		//-----------------------------------------------------------------------------
		// Properties
		//-----------------------------------------------------------------------------

		public ParticleSystem MuzzleFlash {
			get { return muzzleFlash; }
		}

		public GameObject HitEffect {
			get { return hitEffect; }
		}

		//-----------------------------------------------------------------------------
		// Attributes
		//-----------------------------------------------------------------------------

		[SerializeField]
		private ParticleSystem muzzleFlash;

		[SerializeField]
		private GameObject hitEffect;
	}
}

