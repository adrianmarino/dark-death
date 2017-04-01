using UnityEngine;

[RequireComponent (typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
	//-----------------------------------------------------------------------------
	// Engine Methods
	//-----------------------------------------------------------------------------

	void Start ()
	{
		motor = GetComponent <PlayerMotor> ();
		motor.Reset ();
	}

	void Update ()
	{
		UpdateLookRotation ();
		UpdatePosition ();
	}

	//-----------------------------------------------------------------------------
	// Private Methods
	//-----------------------------------------------------------------------------

	void UpdatePosition ()
	{
		Vector3 velocity = Util.VelocityVector.create (
			                   transform, 
			                   KeyboardMovementVariation (), 
			                   speed
		                   );
		motor.Move (velocity);
	}

	void UpdateLookRotation ()
	{
		Vector3 rotation = new Vector3 (0f, MouseMovementVariation ().x, 0f) * lookSensibility;
		motor.Rotate (rotation);

		Vector3 rorateCamera = new Vector3 (MouseMovementVariation ().y, 0f, 0f) * lookSensibility;
		motor.RotateCamera (rorateCamera);
	}

	Vector2 KeyboardMovementVariation ()
	{
		return Util.Input.NextKeyboardHorVerMovementVariation ();
	}

	Vector2 MouseMovementVariation ()
	{
		return Util.Input.NextMouseHorVerMovementVariation ();
	}

	//-----------------------------------------------------------------------------
	// Attributes
	//-----------------------------------------------------------------------------

	[SerializeField]
	private float speed = 5f;

	[SerializeField]
	private float lookSensibility = 3f;

	private PlayerMotor motor;
}
