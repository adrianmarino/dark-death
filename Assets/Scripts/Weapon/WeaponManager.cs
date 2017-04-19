using UnityEngine;
using UnityEngine.Networking;

namespace Fps
{
	public class WeaponManager : NetworkBehaviour
	{
		//-----------------------------------------------------------------------------
		// Events
		//-----------------------------------------------------------------------------

		void OnEnable ()
		{
			if (weaponPrefab != null) // Workaround by unity bug.
				currentWeapon = CreateIntoHolder (weaponPrefab);
		}

		//-----------------------------------------------------------------------------
		// Public Methods
		//-----------------------------------------------------------------------------

		public bool isReady ()
		{
			return currentWeapon != null;  // Workaround by unity bug.
		}

		//-----------------------------------------------------------------------------
		// Private Methods
		//-----------------------------------------------------------------------------

		Weapon CreateIntoHolder (GameObject _weaponPrefab)
		{
			GameObject instance = Instantiate (
				                      _weaponPrefab, 
				                      weaponHolder.position, 
				                      weaponHolder.rotation
			                      );
			instance.transform.SetParent (weaponHolder);

			Weapon weapon = instance.GetComponent<Weapon> ();
			if (weapon == null)
				Debug.Log ("Not found weapon component in weapon prefab!");

			if (isLocalPlayer)
				Util.Layer.SetLayerRecursively (
					instance, 
					LayerMask.NameToLayer (weaponLayerName)
				);

			return weapon;
		}

		GameObject LoadPrefab (string path)
		{
			return (GameObject)Resources.Load (path, typeof(GameObject));
		}

		//-----------------------------------------------------------------------------
		// Properties
		//-----------------------------------------------------------------------------

		public Weapon CurrentWeapon {
			get {
				return currentWeapon;
			}
		}

		//-----------------------------------------------------------------------------
		// Attributes
		//-----------------------------------------------------------------------------

		[SerializeField]
		private string weaponLayerName = "Weapon";

		[SerializeField]
		private Transform weaponHolder;

		[SerializeField]
		private GameObject weaponPrefab;

		private Weapon currentWeapon;
	}
}