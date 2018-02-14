using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Fps.Player;
using UnityEngine.SceneManagement;
using Component = Util.ComponentUtil;

namespace Fps
{
    public class GameManager : MonoBehaviour
    {
        //-----------------------------------------------------------------------------
        // Event Methods
        //-----------------------------------------------------------------------------

        void OnGUI()
        {
            GuiUtils.PlayersWindow(
                players.Values.ToList(),
                new Rect(5, Screen.height - 160, 88, 120)
            );
        }

        void Awake()
        {
            DontDestroyOnLoad(gameObject);

            singleton = this;
            InitSceneCamera();
        }

        //-----------------------------------------------------------------------------
        // Public Methods
        //-----------------------------------------------------------------------------

        public PlayerState GetPlayer(string playerId)
        {
            return players[playerId];
        }

        public void RegisterPlayer(string netId, PlayerState player)
        {
            if (player == null)
                return;

            player.Name = netId;
            players.Add(player.Name, player);
            Debug.LogFormat("{0} Registered", player.Name);
        }

        public void UnregisterPlayer(string playerId)
        {
            if (playerId == "0") return;

            players.Remove(playerId);
            Debug.LogFormat("Unregister {0}", playerId);
        }

        public void SetEnableScenCameraListener(bool value)
        {
            sceneCamera.GetComponent<AudioListener>().enabled = value;
        }

        //-----------------------------------------------------------------------------
        // Private Methods
        //-----------------------------------------------------------------------------

        void InitSceneCamera()
        {
            Component.tryGet<Camera>(sceneCamera, it => it.depth = sceneCameraDepth);
        }

        //-----------------------------------------------------------------------------
        // Attributes
        //-----------------------------------------------------------------------------

        [SerializeField] private GameObject sceneCamera;

        [SerializeField] private int sceneCameraDepth = 1;

        private Dictionary<string, PlayerState> players;

        public static GameManager singleton;

        //-----------------------------------------------------------------------------
        // Constructors
        //-----------------------------------------------------------------------------

        public GameManager()
        {
            players = new Dictionary<string, PlayerState>();
        }
    }
}