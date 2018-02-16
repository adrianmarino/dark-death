using System.Collections.Generic;
using System.Linq;
using Fps.Player;
using UnityEngine;

namespace Fps
{
    public class PlayerStateRepository : MonoBehaviour
    {        
        //-----------------------------------------------------------------------------
        // Public Methods
        //-----------------------------------------------------------------------------

        public PlayerState FindBy(string id)
        {
            return players[id];
        }

        public void Save(string netId, PlayerState player)
        {
            if (player == null)
                return;

            player.Name = netId;
            players.Add(player.Name, player);
            Debug.LogFormat("{0} Registered", player.Name);
        }

        public void Remove(string netId)
        {
            if (netId == "0") return;

            players.Remove(netId);
            Debug.LogFormat("Unregister {0}", netId);
        }

        public List<PlayerState> All()
        {
            return players.Values.ToList();
        }

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
        }

        //-----------------------------------------------------------------------------
        // Attributes
        //-----------------------------------------------------------------------------

        Dictionary<string, PlayerState> players;
        
        public static PlayerStateRepository Instance { get; private set; }

        //-----------------------------------------------------------------------------
        // Constructors
        //-----------------------------------------------------------------------------
    
        public PlayerStateRepository()
        {
            players = new Dictionary<string, PlayerState>();
        }
    }
}