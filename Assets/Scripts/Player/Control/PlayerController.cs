using UnityEngine;

[RequireComponent (typeof(ConfigurableJoint))]
[RequireComponent (typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
	//-----------------------------------------------------------------------------
	// Engine Methods
	//-----------------------------------------------------------------------------

	void Start ()
	{
		motor = GetComponent <PlayerMotor> ();
		joint = GetComponent <ConfigurableJoint> ();
		audioSource = GetComponent<AudioSource> ();
		audioSource.Play ();
		Debug.Log ("Start Music");
		SetJointSettings (jointSpring);
		motor.Reset ();
	}

	void Update ()
	{
		UpdateLookRotation ();
		UpdatePosition ();
	}

	void OnDisable ()
	{
		audioSource.Stop ();
		Debug.Log ("Stop Music");
	}

	//-----------------------------------------------------------------------------
	// Private Methods
	//-----------------------------------------------------------------------------

	void UpdatePosition ()
	{
		UpdateMovement ();
		UpdateJump ();
	}

	void UpdateMovement ()
	{
		Vector3 velocity = Util.VelocityVector.create (transform, KeyboardMovementVariation (), speed);
		motor.Move (velocity);
	}

	void UpdateJump ()
	{
		if (!grounded) {
			grounded = Util.ObjectElement.Grounded (this, floorDistance);
			SetJointSettings (jointSpring);
		}
		Vector3 thruterForceVector = Vector4.zero;
		if (Util.Input.GetJumpButtonDown () && grounded) {
			thruterForceVector = Vector3.up * thrusterForce;
			SetJointSettings (0f);
			grounded = false;
		}
		motor.ApplyTrusterForce (thruterForceVector);
	}

	void UpdateLookRotation ()
	{
		Vector3 rotation = new Vector3 (0f, MouseMovementVariation ().x, 0f) * lookSensibility;
		motor.Rotate (rotation);

		motor.RotateCamera (MouseMovementVariation ().y * lookSensibility);
	}

	Vector2 KeyboardMovementVariation ()
	{
		return Util.Input.NextKeyboardHorVerMovementVariation ();
	}

	Vector2 MouseMovementVariation ()
	{
		return Util.Input.NextMouseHorVerMovementVariation ();
	}


	void SetJointSettings (float _jointSpring)
	{
		joint.yDrive = new JointDrive { positionSpring = _jointSpring, maximumForce = jointMaxForce };
	}

	//-----------------------------------------------------------------------------
	// Attributes
	//-----------------------------------------------------------------------------

	[SerializeField]
	private float speed = 5f;

	[SerializeField]
	private float lookSensibility = 3f;

	[SerializeField]
	private float thrusterForce = 1000f;

	[Header ("Spring settings:")]
	[SerializeField]
	private float jointSpring = 20f;
	[SerializeField]
	private float jointMaxForce = 40f;

	[SerializeField]
	private float floorDistance = 2f;

	private PlayerMotor motor;

	private ConfigurableJoint joint;

	private AudioSource audioSource;

	private bool grounded = true;
}
