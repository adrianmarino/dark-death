using UnityEngine;
using UnityEngine.Networking;

namespace Fps.Weapon
{
	[RequireComponent (typeof(WeaponFactory))]
	public class WeaponManager : NetworkBehaviour
	{
		//-----------------------------------------------------------------------------
		// Event Methods
		//-----------------------------------------------------------------------------

		void Start ()
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

		IWeapon CreateIntoHolder (GameObject _weaponPrefab)
		{
			IWeapon weapon = Factory.InstantiateOnHolder (_weaponPrefab, weaponHolder);

			if (isLocalPlayer)
				Util.Layer.SetLayerRecursively (
					weapon.GameObject, 
					LayerMask.NameToLayer (weaponLayerName)
				);

			return weapon;
		}

		//-----------------------------------------------------------------------------
		// Properties
		//-----------------------------------------------------------------------------

		public IWeapon CurrentWeapon {
			get { return currentWeapon; }
		}

		WeaponFactory Factory {
			get { return GetComponent <WeaponFactory> (); }
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

		private IWeapon currentWeapon;
	}
}