
using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.Linq;

public class PlayerSetup : NetworkBehaviour
{
	//-----------------------------------------------------------------------------
	// Engine Events
	//-----------------------------------------------------------------------------

	void Start ()
	{
		if (!isLocalPlayer)
			DisableAll (components);
		SetupNetID ();
	}

	void OnDisable ()
	{
		Util.Input.ShowCursor ();
	}

	void OnDestroy ()
	{
		Util.Input.ShowCursor ();
	}

	//-----------------------------------------------------------------------------
	// Private Methods
	//-----------------------------------------------------------------------------

	string GetNetId ()
	{
		return "Player " + GetComponent<NetworkIdentity> ().netId;
	}

	static void DisableAll (List<Behaviour> components)
	{
		components.Where (it => it != null).ToList<Behaviour> ().ForEach (it => it.enabled = false);
	}

	void SetupNetID ()
	{
		transform.name = GetNetId ();
	}

	//-----------------------------------------------------------------------------
	// Attributes
	//-----------------------------------------------------------------------------

	[SerializeField]
	private List<Behaviour> components;
}
