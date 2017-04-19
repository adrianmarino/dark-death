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

			GameManager.singleton.SetEnableScenCameraListener (false);
		}

		public override void OnStartClient ()
		{
			base.OnStartClient ();
			GameManager.singleton.RegisterPlayer (NetId (), Player ());
		}

		void OnDisable ()
		{
			if (isLocalPlayer) {
				Util.Input.ShowCursor ();
				GameManager.singleton.SetEnableScenCameraListener (true);
			}
			GameManager.singleton.UnregisterPlayer (NetId ());
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
