using UnityEngine;
using UnityEngine.Networking;

namespace Fps
{
    public class NetworkService : MonoBehaviour
    {
        void Start()
        {
            networkManager = NetworkManager.singleton;
            if (networkManager.matchMaker == null)
                networkManager.StartMatchMaker();            
        }

        public void CreateRoom(string name, string password, uint size)
        {
            networkManager.matchMaker.CreateMatch(
                name,
                size,
                true,
                password,
                "",
                "",
                0,
                0,
                networkManager.OnMatchCreate
            );
            Debug.LogFormat("{0} room created for {1} player.", name, size);
        }
                
        #region Attributes

        private NetworkManager networkManager;

        #endregion
    }
}