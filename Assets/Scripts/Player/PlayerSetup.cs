
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
			components
				.Where ((it) => it != null)
				.ToList<Behaviour> ()
				.ForEach (it => it.enabled = false);
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
	// Attributes
	//-----------------------------------------------------------------------------

	[SerializeField]
	private List<Behaviour> components;
}
