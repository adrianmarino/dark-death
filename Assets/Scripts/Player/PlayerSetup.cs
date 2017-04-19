using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

namespace Fps
{
	public class PlayerSetup : NetworkBehaviour
	{
		//-----------------------------------------------------------------------------
		// Engine Events
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

		Player Player ()
		{
			return GetComponent<Player> ();
		}

		//-----------------------------------------------------------------------------
		// Attributes
		//-----------------------------------------------------------------------------

		[SerializeField]
		private List<Behaviour> components;
	}
}
