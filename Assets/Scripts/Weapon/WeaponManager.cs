using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.Linq;
using Util;

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
			// Workaround by unity 
			if (weaponPrefabs.Count > 0)
				Use (Weapons.First ());
		}

		void Update ()
		{
			if (Util.Input.NextWeaponButton ())
				Use (Weapons.Next (currentWeapon));
			else if (Util.Input.PreviousWeaponButton ())
				Use (Weapons.Previous (currentWeapon));
		}

		//-----------------------------------------------------------------------------
		// Public Methods
		//-----------------------------------------------------------------------------

		public bool isReady ()
		{
			return currentWeapon != null;  // Workaround by unity bug.
		}

		public int RemainAmmo ()
		{
			return CurrentWeapon.RemainAmmo;
		}

		public static WeaponManager GetFromPlayer ()
		{
			GameObject player = GameObject.Find ("Player");
			if (player == null) {
				Debug.Log ("Not found a player in scene!");
				return null;
			}
			WeaponManager weaponManager = player.GetComponent<WeaponManager> ();
			if (weaponManager == null) {
				Debug.Log ("Not found weapon manager componente in player object!");
				return null;
			}
			return weaponManager;
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

		List<IWeapon> CreateWeapons ()
		{
			return weaponPrefabs.Select (CreateIntoHolder).Select ((weapon) => {
				weapon.GameObject.SetActive (false);
				return weapon;
			}).ToList ();
		}

		void Use (IWeapon weapon)
		{
			if (currentWeapon != null)
				currentWeapon.GameObject.SetActive (false);

			currentWeapon = weapon;
			currentWeapon.GameObject.SetActive (true);
		}
			
		//-----------------------------------------------------------------------------
		// Properties
		//-----------------------------------------------------------------------------

		public List<IWeapon> Weapons {
			get {
				if (weapons == null)
					weapons = CreateWeapons ();
				return weapons;
			}
		}

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
		private List<GameObject> weaponPrefabs;

		private List<IWeapon> weapons;

		private IWeapon currentWeapon;
	}
}