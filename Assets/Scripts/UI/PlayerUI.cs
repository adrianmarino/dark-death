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
            finishMathcModal.Show(
                "Do you want finish match?",
                () => {
                    NetworkService.Instance.LeaveMatch((success, info) =>
                    {
                    });
                },
                playerPauseManager.Resume
            );
        }

        #region Attributes

        [SerializeField] private YesNoModal finishMathcModal;
        
        [SerializeField] private PlayerPauseManager playerPauseManager;

        #endregion
    }
}