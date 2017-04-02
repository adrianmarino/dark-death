using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
	//-----------------------------------------------------------------------------
	// Engine methods
	//-----------------------------------------------------------------------------

	void Start ()
	{
		Util.Input.HideCursor ();
		_rigidbody = GetComponent<Rigidbody> ();
		velocity = rotation = Vector3.zero;
		cameraRotation = 0f;
	}

	void FixedUpdate ()
	{
		Util.Input.HideCursor ();
		UpdatePosition ();
		UpdateRotation ();
	}

	//-----------------------------------------------------------------------------
	// Public Methods
	//-----------------------------------------------------------------------------

	public void Reset ()
	{
		Move (Vector3.zero);
		Rotate (Vector3.zero);
		if (_camera != null)
			_camera.transform.Rotate (Vector3.zero);
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
		Util.Rigidbody.Move (_rigidbody, velocity);
	}

	void UpdateRotation ()
	{
		Util.Rigidbody.Rotate (_rigidbody, rotation);
		SetCameraRotation (cameraRotation);
	}

	void SetCameraRotation (float xRotation)
	{
		if (_camera != null)
			_camera.transform.Rotate (new Vector3 (-xRotation, 0f, 0f));		
	}

	//-----------------------------------------------------------------------------
	// Attributes
	//-----------------------------------------------------------------------------

	[SerializeField]
	private Camera _camera;

	private Rigidbody _rigidbody;

	private Vector3 velocity, rotation;

	private float cameraRotation;
}
