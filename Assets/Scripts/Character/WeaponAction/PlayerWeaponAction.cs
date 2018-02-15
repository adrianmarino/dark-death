using UnityEngine;
using Fps.Weapon;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Fps.Player
{
    [RequireComponent(typeof(WeaponManager))]
    public abstract class PlayerWeaponAction : NetworkBehaviour, IPausable
    {
        //-----------------------------------------------------------------------------
        // Private Methods
        //-----------------------------------------------------------------------------

        protected void UpdateAmmoPanel()
        {
            ammoPanel.text =
                Weapon.RemainAmmo.ToString().PadLeft(2, '0') + " " + Weapon.Name + " " + Weapon.State;
        }

        //-----------------------------------------------------------------------------
        // Properties
        //-----------------------------------------------------------------------------

        protected IWeapon Weapon
        {
            get { return WeaponManager.CurrentWeapon; }
        }

        protected WeaponManager WeaponManager
        {
            get { return GetComponent<WeaponManager>(); }
        }

        public bool Pause
        {
            get { return pause; }
            set { pause = value; }
        }
        
        //-----------------------------------------------------------------------------
        // Attributes
        //-----------------------------------------------------------------------------

        [SerializeField] private Text ammoPanel;

        [SerializeField] private bool pause;
    }
}