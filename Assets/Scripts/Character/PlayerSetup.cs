using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

namespace Fps.Player
{
    public class PlayerSetup : NetworkBehaviour
    {
        //-----------------------------------------------------------------------------
        // Event Methods
        //-----------------------------------------------------------------------------

        private void Start()
        {
            if (isLocalPlayer)
                Player().Setup();
            else
                Util.BehaviourUtil.DisableAll(components);

            GameManager.SetEnableScenCameraListener(false);
        }

        public override void OnStartClient()
        {
            GameManager.RegisterPlayer(NetId(), Player());
        }

        private void OnDisable()
        {
            if (isLocalPlayer)
            {
                Util.Input.ShowCursor();
                GameManager.SetEnableScenCameraListener(true);
            }

            GameManager.UnregisterPlayer(NetId());
        }

        //-----------------------------------------------------------------------------
        // Properties
        //-----------------------------------------------------------------------------

        private static GameManager GameManager
        {
            get { return GameManager.Instance; }
        }

        //-----------------------------------------------------------------------------
        // Private Methods
        //-----------------------------------------------------------------------------

        private string NetId()
        {
            return GetComponent<NetworkIdentity>().netId.ToString();
        }

        private PlayerState Player()
        {
            return GetComponent<PlayerState>();
        }

        //-----------------------------------------------------------------------------
        // Attributes
        //-----------------------------------------------------------------------------

        [SerializeField] private List<Behaviour> components;
    }
}
