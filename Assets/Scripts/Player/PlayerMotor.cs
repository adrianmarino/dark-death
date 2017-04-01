using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
	//-----------------------------------------------------------------------------
	// Engine methods
	//-----------------------------------------------------------------------------

	void Start ()
	{
		HideCursor ();
		rigidbody = GetComponent<Rigidbody> ();
		velocity = rotation = cameraRotation = Vector3.zero;
	}

	void FixedUpdate ()
	{
		HideCursor ();
		UpdatePosition ();
		UpdateRotation ();
	}

	//-----------------------------------------------------------------------------
	// Public Methods
	//-----------------------------------------------------------------------------

	public void HideCursor ()
	{
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}


	public void Reset ()
	{
		Move (Vector3.zero);
		Rotate (Vector3.zero);
		RotateCamera (Vector3.zero);
	}

	public void Move (Vector3 velocity)
	{
		this.velocity = velocity;
	}

	public void Rotate (Vector3 rotation)
	{
		this.rotation = rotation;
	}

	public void RotateCamera (Vector3 cameraRotation)
	{
		this.cameraRotation = cameraRotation;
	}

	//-----------------------------------------------------------------------------
	// Private Methods
	//-----------------------------------------------------------------------------

	void UpdatePosition ()
	{
		Util.Rigidbody.Move (rigidbody, velocity);
	}

	void UpdateRotation ()
	{
		Util.Rigidbody.Rotate (rigidbody, rotation);
		if (camera != null)
			camera.transform.Rotate (-cameraRotation);
	}

	//-----------------------------------------------------------------------------
	// Attributes
	//-----------------------------------------------------------------------------

	[SerializeField]
	private Camera camera;

	private Rigidbody rigidbody;

	private Vector3 velocity, rotation, cameraRotation;
}
