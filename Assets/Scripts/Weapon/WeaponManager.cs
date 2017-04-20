using UnityEngine;
using UnityEngine.Networking;

namespace Fps
{
	public class WeaponManager : NetworkBehaviour
	{
		//-----------------------------------------------------------------------------
		// Events
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

		Weapon CreateIntoHolder (GameObject _weaponPrefab)
		{
			Weapon weapon = Weapon.InstantiateOnHolder (_weaponPrefab, weaponHolder);

			if (isLocalPlayer)
				Util.Layer.SetLayerRecursively (
					weapon.gameObject, 
					LayerMask.NameToLayer (weaponLayerName)
				);

			return weapon;
		}

		//-----------------------------------------------------------------------------
		// Properties
		//-----------------------------------------------------------------------------

		public Weapon CurrentWeapon {
			get { return currentWeapon; }
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