using System.Collections.Generic;
using System.Linq;
using Fps.Player;
using UnityEngine;

namespace Fps
{
    public class PlayerStateRepository
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
        
        //-----------------------------------------------------------------------------
        // Attributes
        //-----------------------------------------------------------------------------

        private readonly Dictionary<string, PlayerState> players;
  
        //-----------------------------------------------------------------------------
        // Constructors
        //-----------------------------------------------------------------------------
    
        public PlayerStateRepository()
        {
            players = new Dictionary<string, PlayerState>();
        }
    }
}
