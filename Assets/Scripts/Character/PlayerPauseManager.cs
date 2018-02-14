using UnityEngine;

namespace Fps.Player
{
    [RequireComponent(typeof(PlayerController))]
    [RequireComponent(typeof(PlayerMotor))]
    [RequireComponent(typeof(PlayerState))]
    [RequireComponent(typeof(PlayerShootWeaponAction))]
    [RequireComponent(typeof(PlayerReloadWeaponAction))]
    public class PlayerPauseManager : MonoBehaviour
    {
        public void Pause()
        {
            Pause(true);
        }

        public void Resume()
        {
            Pause(false);
        }
        
        private void Pause(bool value)
        {
            GetComponent<PlayerController>().Pause = value;
            GetComponent<PlayerMotor>().Pause = value;
            GetComponent<PlayerState>().Pause = value;
            GetComponent<PlayerShootWeaponAction>().Pause = value;
            GetComponent<PlayerReloadWeaponAction>().Pause = value;
        }
    }
}