using UnityEngine.Networking;
using UnityEngine;

public class WeaponManager : NetworkBehaviour
{
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
			instance.layer = LayerMask.NameToLayer (weaponLayerName);
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
