namespace Fps.Player
{
	public class PlayerReloadWeaponAction : PlayerWeaponAction
	{
		//-----------------------------------------------------------------------------
		// Event Methods
		//-----------------------------------------------------------------------------

		void Update ()
		{
			if (WeaponManager.isReady () && Util.Input.GetReloadButton ()) {
				Weapon.Reload ();
				UpdateAmmoPanel ();
			}
		}
	}
}