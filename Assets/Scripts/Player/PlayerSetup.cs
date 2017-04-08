using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.Linq;
using Fps;

public class PlayerSetup : NetworkBehaviour
{
	//-----------------------------------------------------------------------------
	// Engine Events
	//-----------------------------------------------------------------------------

	void Start ()
	{
		if (!isLocalPlayer)
			Util.Behaviour.DisableAll (components);
		else
			Player ().Setup ();
	}

	public override void OnStartClient ()
	{
		GameManager.RegisterPlayer (NetId (), Player ());
	}

	void OnDisable ()
	{
		Util.Input.ShowCursor ();
		GameManager.UnregisterPlayer (NetId ());
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
