using Fps.Player;
using UnityEngine;
using Util.Component.UI;

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
                .OnYes(modal =>GameManager.CloseMatch())
                .OnNo(modal => {
                    modal.Close();
                    playerPauseManager.Resume();
                })
                .NotClose()
                .Show();
        }

        private static GameManager GameManager
        {
            get { return GameManager.Instance; }
        }

        #region Attributes

        [SerializeField] private YesNoModal yesNoModal;

        [SerializeField] private PlayerPauseManager playerPauseManager;

        #endregion
    }
}