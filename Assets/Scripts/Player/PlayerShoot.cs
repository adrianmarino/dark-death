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
		if (Physics.Raycast (CameraPosition (), CameraDirection (), out hit, oponentMask)) {
			Debug.Log ("Inpact to " + hit.collider.gameObject.tag);
		}
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
	// Attributes
	//-----------------------------------------------------------------------------

	[SerializeField]
	private LayerMask oponentMask;

	[SerializeField]
	private Camera _camera;

	private PlayerWeapon weapon;

}
