using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Fps.Player;
using UnityEngine.Networking.Match;
using UnityEngine.SceneManagement;
using Util.Component.UI;
using Component = Util.ComponentUtil;

namespace Fps
{
    public class GameManager : MonoBehaviour
    {
        //-----------------------------------------------------------------------------
        // Public Methods
        //-----------------------------------------------------------------------------

        public void RegisterPlayer(string netId, PlayerState playerState)
        {
            PlayerStateRepository.Instance.Save(netId, playerState);
        }
        
        public void UnregisterPlayer(string netId)
        {
           PlayerStateRepository.Instance.Remove(netId);
        }
        
        public PlayerState GetPlayer(string netId)
        {
            return PlayerStateRepository.Instance.FindBy(netId);
        }

        public List<PlayerState> Players()
        {
            return PlayerStateRepository.Instance.All();
        }
        
        public void SetEnableScenCameraListener(bool value)
        {
            sceneCamera.GetComponent<AudioListener>().enabled = value;
        }

        public void SearchMatch(string name, NetworkMatch.DataResponseDelegate<List<MatchInfoSnapshot>> callback)
        {
            NetworkService.Instance.SearchMatch(name, 20, callback);
        }
        
        public void JoinMatch(MatchInfoSnapshot matchInfo, NetworkMatch.DataResponseDelegate<MatchInfo> callback)
        {
            NetworkService.Instance.JoinMatch(matchInfo, (success, info, data) => {
                if (success)
                    SceneFadeManager.Instance.FadeOut();
                callback(success, info, data);
            });
        }
        
        public void StartMatch(string matchName, uint maxPlayers)
        {
            SceneFadeManager.Instance.FadeOut();
            NetworkService.Instance.CreateMatch(matchName, maxPlayers);
        }
        
        public void CloseMatch()
        {
            SceneFadeManager.Instance.FadeOut();
            NetworkService.Instance.LeaveMatch();
        }

        //-----------------------------------------------------------------------------
        // Private Methods
        //-----------------------------------------------------------------------------

        void Awake()
        {
            if (Instance == null)
                Instance = this;
            else {
                // Used when reloading scene to ensure that only exist one GameManager instance.
                Destroy(gameObject);
            }

            // Sets this to not be destroyed when reloading scene.
            DontDestroyOnLoad(gameObject);
            
            InitSceneCamera();
        }
        
        void InitSceneCamera()
        {
            Component.tryGet<Camera>(sceneCamera, it => it.depth = sceneCameraDepth);
        }

        public static GameManager Instance { get; private set; }

        //-----------------------------------------------------------------------------
        // Attributes
        //-----------------------------------------------------------------------------

        [SerializeField] private GameObject sceneCamera;

        [SerializeField] private int sceneCameraDepth = 1;
    }
}