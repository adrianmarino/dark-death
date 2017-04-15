using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using Fps;

public class PlayerSetup : NetworkBehaviour
{
	//-----------------------------------------------------------------------------
	// Engine Events
	//-----------------------------------------------------------------------------

	void Start ()
	{
		if (isLocalPlayer) {
			Player ().Setup ();
		} else
			Util.Behaviour.DisableAll (components);

		GameManager.singleton.SetEnableScenCameraListener (false);
	}

	public override void OnStartClient ()
	{
		base.OnStartClient ();
		GameManager.singleton.RegisterPlayer (NetId (), Player ());
	}

	void OnDisable ()
	{
		Util.Input.ShowCursor ();
		GameManager.singleton.UnregisterPlayer (NetId ());
		GameManager.singleton.SetEnableScenCameraListener (true);
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
