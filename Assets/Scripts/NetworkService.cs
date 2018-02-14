using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

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

        public void SearchMatch(
            string name, 
            int pages, 
            NetworkMatch.DataResponseDelegate<List<MatchInfoSnapshot>> callback
        )
        {             
            networkManager.matchMaker.ListMatches(
                0, 
                pages, 
                name, 
                false,
                0,
                0,
                callback
            );
        }

        public void LeaveMatch()
        {
            LeaveMatch((success, info) => { });
        }

        public void LeaveMatch(NetworkMatch.BasicResponseDelegate callback)
        {
            var currentMatchInfo = networkManager.matchInfo;
            networkManager.matchMaker.DropConnection(
                currentMatchInfo.networkId,
                currentMatchInfo.nodeId,
                0,
                (success, info) => {
                    callback(success, info);
                    networkManager.OnDropConnection(success, info);
                    networkManager.StopHost();
                }
            );
        }
        
        public void JoinMatch(MatchInfoSnapshot match, NetworkMatch.DataResponseDelegate<MatchInfo> callback)
        {
            networkManager.matchMaker.JoinMatch(
                match.networkId,
                NO_PASSWORD,
                "",
                "",
                0,
                0,
                (success, info, data) => {
                    callback(success, info, data);
                    networkManager.OnMatchJoined(success, info, data);
                }
            );
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

        public static NetworkService Instance
        {
            get
            {
                var gameObject = GameObject.Find("NetworkService");
                return gameObject.GetComponent<NetworkService>();
            }
        }
        
        private const string NO_PASSWORD = "";
    }
}