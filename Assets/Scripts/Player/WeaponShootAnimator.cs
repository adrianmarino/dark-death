using UnityEngine;

public class WeaponShootAnimator : MonoBehaviour
{
	//-----------------------------------------------------------------------------
	// Events
	//-----------------------------------------------------------------------------

	void Update ()
	{
		if (Util.Input.GetFireButton ())
			Animate ();
	}

	//-----------------------------------------------------------------------------
	// Private Methods
	//-----------------------------------------------------------------------------

	void Animate ()
	{
		transform.position = new Vector3 (
			transform.position.x,
			transform.position.y,
			15f
		);
	}

	//-----------------------------------------------------------------------------
	// Attributes
	//-----------------------------------------------------------------------------

	// The minimum vibration power
	[SerializeField]
	private float minVibration = 15f;

	// The maximum vibration power
	[SerializeField]
	private float maxVibration = 15f;

	private bool firing;
}


