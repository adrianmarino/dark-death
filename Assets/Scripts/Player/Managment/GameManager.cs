using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

namespace Fps
{
	public class GameManager : MonoBehaviour
	{
		void Awake ()
		{
			singleton = this;
			InitSceneCamera ();
		}

		//-----------------------------------------------------------------------------
		// Public Methods
		//-----------------------------------------------------------------------------

		public Player GetPlayer (string playerId)
		{
			return players [playerId];
		}

		void InitSceneCamera ()
		{
			sceneCamera.GetComponent<Camera> ().depth = sceneCameraDepth;
			sceneCamera.GetComponent<BlurOptimized> ().enabled = true;
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

		public void SetEnableScenCameraListener (bool value)
		{
			sceneCamera.GetComponent<AudioListener> ().enabled = value;
		}

		//-----------------------------------------------------------------------------
		// Constants
		//-----------------------------------------------------------------------------

		private const string ID_PREFIX = "Player ";

		//-----------------------------------------------------------------------------
		// Attributes
		//-----------------------------------------------------------------------------

		[SerializeField]
		private GameObject sceneCamera;


		[SerializeField]
		private int sceneCameraDepth = 1;

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