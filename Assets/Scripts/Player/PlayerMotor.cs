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
		velocity = rotation = Vector3.zero;
		cameraRotation = 0f;
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
		if (camera != null)
			camera.transform.Rotate (Vector3.zero);
	}

	public void Move (Vector3 velocity)
	{
		this.velocity = velocity;
	}

	public void Rotate (Vector3 rotation)
	{
		this.rotation = rotation;
	}

	public void RotateCamera (float cameraRotation)
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
		SetCameraRotation (cameraRotation);
	}

	void SetCameraRotation (float xRotation)
	{
		if (camera != null)
			camera.transform.Rotate (new Vector3 (-xRotation, 0f, 0f));		
	}

	//-----------------------------------------------------------------------------
	// Attributes
	//-----------------------------------------------------------------------------

	[SerializeField]
	private Camera camera;

	private Rigidbody rigidbody;

	private Vector3 velocity, rotation;

	private float cameraRotation;
}
