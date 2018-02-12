using System;
using UnityEngine;
using UnityEngine.Networking;

public class HostGame : MonoBehaviour
{
    void Start()
    {
        _networkManager = NetworkManager.singleton;   
        if (_networkManager.matchMaker == null)
            _networkManager.StartMatchMaker();
    }
   
    public void CreateRoom()
    {
        if (String.IsNullOrEmpty(_roomName) && String.IsNullOrEmpty(_roomPassword) && _roomSize >= 2) return;

        Debug.LogFormat("Create {0} room for {1} player.");
        _networkManager.matchMaker.CreateMatch(
            _roomName, 
            _roomSize, 
            true, 
            _roomPassword,
            "",
            "",
            0,
            0,
            _networkManager.OnMatchCreate
        );
    }

    #region Attributes

    [SerializeField]
    private uint _roomSize = 6;

    [SerializeField]
    private string _roomName;
    
    [SerializeField]
    private string _roomPassword;

    private NetworkManager _networkManager;

    #endregion
}

