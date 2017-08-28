using UnityEngine;
using Fps.Weapon;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Fps.Player
{
	[RequireComponent (typeof(WeaponManager))]
	public abstract class PlayerWeaponAction : NetworkBehaviour
	{
		//-----------------------------------------------------------------------------
		// Private Methods
		//-----------------------------------------------------------------------------

		protected void UpdateAmmoPanel ()
		{
			ammoPanel.text = Weapon.RemainAmmo + " " + Weapon.Name + " " + Weapon.State;
		}

		//-----------------------------------------------------------------------------
		// Properties
		//-----------------------------------------------------------------------------

		protected IWeapon Weapon {
			get { return WeaponManager.CurrentWeapon; }
		}

		protected WeaponManager WeaponManager {
			get { return GetComponent<WeaponManager> (); }
		}

		//-----------------------------------------------------------------------------
		// Attributes
		//-----------------------------------------------------------------------------

		[SerializeField]
		private Text ammoPanel;
	}
}