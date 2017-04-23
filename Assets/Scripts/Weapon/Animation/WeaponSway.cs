using UnityEngine;

public class WeaponSway : MonoBehaviour
{
	void Update ()
	{
		transform.localRotation = Quaternion.Slerp (
			transform.localRotation,
			MousePoint (),
			speed * Time.deltaTime
		);
	}

	//-----------------------------------------------------------------------------
	// Private Methods
	//-----------------------------------------------------------------------------

	Quaternion MousePoint ()
	{
		return Quaternion.Euler (
			-Util.Input.MouseVercalMovementDelta (), 
			Util.Input.MouseHorizontalMovementDelta (), 
			0
		);
	}

	//-----------------------------------------------------------------------------
	// Attributes
	//-----------------------------------------------------------------------------

	private float mouseX, mouseY;

	private Quaternion mousePoint;

	[SerializeField]
	private float speed = 1.5f;
}

