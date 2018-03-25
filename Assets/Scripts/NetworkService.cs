using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

namespace Fps
{
    public class NetworkService
    {
        public void SearchMatch(
            string name, 
            int pages, 
            NetworkMatch.DataResponseDelegate<List<MatchInfoSnapshot>> callback
        )
        {
            NetworkManager.matchMaker.ListMatches(
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
            var currentMatchInfo = NetworkManager.matchInfo;
            NetworkManager.matchMaker.DropConnection(
                currentMatchInfo.networkId,
                currentMatchInfo.nodeId,
                0,
                (success, info) => {
                    callback(success, info);
                    NetworkManager.OnDropConnection(success, info);
                    NetworkManager.StopHost();
                }
            );
        }
        
        public void JoinMatch(MatchInfoSnapshot match, NetworkMatch.DataResponseDelegate<MatchInfo> callback)
        {
            NetworkManager.matchMaker.JoinMatch(
                match.networkId,
                NO_PASSWORD,
                "",
                "",
                0,
                0,
                (success, info, data) => {
                    callback(success, info, data);
                    NetworkManager.OnMatchJoined(success, info, data);
                }
            );
        }
        
        public void CreateMatch(string name, uint size)
        {
            CreateMatch(name, NO_PASSWORD, size);
        }
        
        public void CreateMatch(string name, string password, uint size)
        {
            NetworkManager.matchMaker.CreateMatch(
                name,
                size,
                true,
                password,
                "",
                "",
                0,
                0,
                NetworkManager.OnMatchCreate
            );
            Debug.LogFormat("{0} room created for {1} player.", name, size);
        }


        private static NetworkManager NetworkManager
        {
            get {
                NetworkManager networkManager = NetworkManager.singleton;
                if (networkManager.matchMaker == null)
                    networkManager.StartMatchMaker();
                return networkManager;
            }
        }

        private const string NO_PASSWORD = "";
    }
}
