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
			DisableAll (components);
	}

	public override void OnStartClient ()
	{
		GameManager.RegisterPlayer (NetId (), GetComponent<Player> ());
	}

	void OnDisable ()
	{
		Util.Input.ShowCursor ();
		GameManager.UnregisterPlayer (NetId ());
	}

	void OnDestroy ()
	{
		Util.Input.ShowCursor ();
	}

	//-----------------------------------------------------------------------------
	// Private Methods
	//-----------------------------------------------------------------------------

	string NetId ()
	{
		return GetComponent<NetworkIdentity> ().netId.ToString ();
	}

	static void DisableAll (List<Behaviour> components)
	{
		components.Where (it => it != null).ToList<Behaviour> ().ForEach (it => it.enabled = false);
	}

	//-----------------------------------------------------------------------------
	// Attributes
	//-----------------------------------------------------------------------------

	[SerializeField]
	private List<Behaviour> components;
}
