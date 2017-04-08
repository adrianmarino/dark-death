using System.Collections.Generic;
using UnityEngine;

namespace Fps
{
	public class GameManager : MonoBehaviour
	{
		void OnGU_I ()
		{
			GUILayout.BeginArea (new Rect (200, 200, 200, 200));
			GUILayout.BeginVertical ();

			foreach (string playerID in players.Keys)
				GUILayout.Label (playerID + "  -  " + players [playerID].transform.name);

			GUILayout.EndVertical ();
			GUILayout.EndArea ();
		}

		//-----------------------------------------------------------------------------
		// Public Methods
		//-----------------------------------------------------------------------------

		public static Player GetPlayer (string playerId)
		{
			return players [playerId];
		}

		public static void RegisterPlayer (string netId, Player player)
		{
			string playerId = ID_PREFIX + netId;
			player.transform.name = playerId;

			players.Add (playerId, player);
			Debug.Log (playerId + " Registered!");
		}

		public static void UnregisterPlayer (string playerId)
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

		private static Dictionary<string, Player> players = new Dictionary <string, Player> ();
	}
}