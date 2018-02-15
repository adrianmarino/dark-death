using Fps.Player;
using UnityEngine;
    
namespace Fps.UI
{
    public class PlayerUI : MonoBehaviour
    {
        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Escape)) return;

            playerPauseManager.Pause();

            yesNoModal.Setup()
                .Question("Do you want finish match?")
                .OnYes(modal => NetworkService.Instance.LeaveMatch())
                .OnNo(modal => {
                    modal.Close();
                    playerPauseManager.Resume();
                })
                .NotClose()
                .Show();
        }

        #region Attributes

        [SerializeField] private YesNoModal yesNoModal;

        [SerializeField] private PlayerPauseManager playerPauseManager;

        #endregion
    }
}