using System.Collections.Generic;
using UnityEngine;

namespace Fps
{
	public class GameManager : MonoBehaviour
	{
		void Awake ()
		{
			singleton = this;
		}

		//-----------------------------------------------------------------------------
		// Public Methods
		//-----------------------------------------------------------------------------

		public Player GetPlayer (string playerId)
		{
			return players [playerId];
		}

		public void RegisterPlayer (string netId, Player player)
		{
			if (player == null) {
				Debug.Log ("Does not register a null player!");
				return;
			}

			string playerId = ID_PREFIX + netId;
			player.transform.name = playerId;

			players.Add (playerId, player);
			Debug.Log (playerId + " Registered!");
		}

		public void UnregisterPlayer (string playerId)
		{
			players.Remove (playerId);
		}

		//-----------------------------------------------------------------------------
		// Constants
		//-----------------------------------------------------------------------------

		private const string ID_PREFIX = "Player ";

		//-----------------------------------------------------------------------------
		// Attributes
		//-----------------------------------------------------------------------------

		private Dictionary<string, Player> players = new Dictionary <string, Player> ();

		public static GameManager singleton = null;

		//-----------------------------------------------------------------------------
		// Constructors
		//-----------------------------------------------------------------------------

		public GameManager ()
		{
			players = new Dictionary <string, Player> ();
		}
	}
}