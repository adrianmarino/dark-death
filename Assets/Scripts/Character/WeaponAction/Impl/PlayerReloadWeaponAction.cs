using UnityEngine;

namespace Fps.Player
{
    public class PlayerReloadWeaponAction : PlayerWeaponAction
    {
        //-----------------------------------------------------------------------------
        // Event Methods
        //-----------------------------------------------------------------------------

        void Update()
        {
            if (!WeaponManager.isReady() || !PressReload() || !isLocalPlayer) return;

            Weapon.Reload();
            UpdateAmmoPanel();
        }

        //-----------------------------------------------------------------------------
        // Private Methods
        //-----------------------------------------------------------------------------

        static bool PressReload()
        {
            return (Util.Input.GetReloadButton() || Input.GetMouseButtonDown(1));
        }
    }
}