using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;
using System.Linq;
using Fps.Character;

namespace Fps
{
	public class GameManager : MonoBehaviour
	{
		//-----------------------------------------------------------------------------
		// Event Methods
		//-----------------------------------------------------------------------------

		void OnGUI ()
		{
			GUIUtils.PlayersWindow (
				players.Values.ToList<Player> (),
				new Rect (5, Screen.height - 155, 100, 120)
			);
		}

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

		public void RegisterPlayer (string netId, Player player)
		{
			if (player == null)
				return;

			player.Name = netId;
			players.Add (player.Name, player);
			Debug.Log (player.Name + " Registered");
		}

		public void UnregisterPlayer (string playerId)
		{
			if (playerId == "0")
				return;

			players.Remove (playerId);
			Debug.Log ("Unregister " + playerId);
		}

		public void SetEnableScenCameraListener (bool value)
		{
			sceneCamera.GetComponent<AudioListener> ().enabled = value;
		}

		//-----------------------------------------------------------------------------
		// Private Methods
		//-----------------------------------------------------------------------------

		void InitSceneCamera ()
		{
			sceneCamera.GetComponent<Camera> ().depth = sceneCameraDepth;
			sceneCamera.GetComponent<BlurOptimized> ().enabled = true;
		}

		//-----------------------------------------------------------------------------
		// Attributes
		//-----------------------------------------------------------------------------

		[SerializeField]
		private GameObject sceneCamera;

		[SerializeField]
		private int sceneCameraDepth = 1;

		private Dictionary<string, Player> players;

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