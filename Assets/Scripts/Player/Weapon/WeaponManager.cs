using UnityEngine.Networking;
using UnityEngine;
using Fps;

public class WeaponManager : NetworkBehaviour
{
	void Awake ()
	{
		currentWeapon = primaryWeapon;
	}

	void Start ()
	{
		EquipWeapon (primaryWeapon);
	}

	//-----------------------------------------------------------------------------
	// Public Methods
	//-----------------------------------------------------------------------------

	public PlayerWeapon GetCurrentWeapon ()
	{
		return currentWeapon;
	}

	//-----------------------------------------------------------------------------
	// Private Methods
	//-----------------------------------------------------------------------------

	void EquipWeapon (PlayerWeapon weapon)
	{
		currentWeapon = weapon;
		CreateIntoHolder (weapon);
	}

	GameObject CreateIntoHolder (PlayerWeapon weapon)
	{
		GameObject instance = Instantiate (
			                      weapon.graphics, 
			                      weaponHolder.position, 
			                      weaponHolder.rotation
		                      );
		instance.transform.SetParent (weaponHolder);

		if (isLocalPlayer)
			Util.Layer.SetLayerRecursively (
				instance, 
				LayerMask.NameToLayer (weaponLayerName)
			);

		return instance;
	}

	//-----------------------------------------------------------------------------
	// Attributes
	//-----------------------------------------------------------------------------

	[SerializeField]
	private PlayerWeapon primaryWeapon;

	[SerializeField]
	private string weaponLayerName = "Weapon";

	[SerializeField]
	private Transform weaponHolder;

	private PlayerWeapon currentWeapon;
}
