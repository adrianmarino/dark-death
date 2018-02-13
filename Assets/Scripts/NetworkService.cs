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

        public void CreateMatch(string name, uint size)
        {
            CreateMatch(name, NO_PASSWORD, size);
        }
        
        public void CreateMatch(string name, string password, uint size)
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
        
        private const string NO_PASSWORD = "";
    }
}