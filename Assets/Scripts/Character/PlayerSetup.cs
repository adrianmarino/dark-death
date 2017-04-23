using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

namespace Fps.Player
{
	public class PlayerSetup : NetworkBehaviour
	{
		//-----------------------------------------------------------------------------
		// Event Methods
		//-----------------------------------------------------------------------------

		void Start ()
		{
			if (isLocalPlayer)
				Player ().Setup ();
			else
				Util.Behaviours.DisableAll (components);

			GameManager.SetEnableScenCameraListener (false);
		}

		public override void OnStartClient ()
		{
			GameManager.RegisterPlayer (NetId (), Player ());
		}

		void OnDisable ()
		{
			if (isLocalPlayer) {
				Util.Input.ShowCursor ();
				GameManager.SetEnableScenCameraListener (true);
			}
			GameManager.UnregisterPlayer (NetId ());
		}

		//-----------------------------------------------------------------------------
		// Properties
		//-----------------------------------------------------------------------------

		GameManager GameManager {
			get { return GameManager.singleton; }
		}

		//-----------------------------------------------------------------------------
		// Private Methods
		//-----------------------------------------------------------------------------

		string NetId ()
		{
			return GetComponent<NetworkIdentity> ().netId.ToString ();
		}

		PlayerState Player ()
		{
			return GetComponent<PlayerState> ();
		}

		//-----------------------------------------------------------------------------
		// Attributes
		//-----------------------------------------------------------------------------

		[SerializeField]
		private List<Behaviour> components;
	}
}
