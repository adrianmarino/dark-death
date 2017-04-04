using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
	void Update ()
	{
		if (Util.Input.GetFireButton ())
			Shoot ();
	}

	//-----------------------------------------------------------------------------
	// Private Methods
	//-----------------------------------------------------------------------------

	void Shoot ()
	{
		RaycastHit hit;
		bool intersect = Physics.Raycast (CameraPosition (), CameraDirection (), out hit, oponentMask);

		if (intersect && hit.collider.tag == PLAYER_TAG)
			Debug.Log (hit.collider.name + " has been shot!");
	}

	Vector3 CameraPosition ()
	{
		return _camera.transform.position;
	}

	Vector3 CameraDirection ()
	{
		return _camera.transform.forward;
	}

	//-----------------------------------------------------------------------------
	// Constants
	//-----------------------------------------------------------------------------

	private const string PLAYER_TAG = "Player";

	//-----------------------------------------------------------------------------
	// Attributes
	//-----------------------------------------------------------------------------

	[SerializeField]
	private LayerMask oponentMask;

	[SerializeField]
	private Camera _camera;

	private PlayerWeapon weapon;
}
