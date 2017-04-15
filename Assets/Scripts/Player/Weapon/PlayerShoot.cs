using UnityEngine;
using UnityEngine.Networking;

namespace Fps
{
	[RequireComponent (typeof(WeaponManager))]
	public class PlayerShoot : NetworkBehaviour
	{
		void Update ()
		{
			currentWeapon = weaponManager.GetCurrentWeapon ();
			if (Util.Input.GetFireButton ())
				Shoot ();
		}

		void Start ()
		{
			weaponManager = GetComponent<WeaponManager> ();
		}

		//-----------------------------------------------------------------------------
		// Private Methods
		//-----------------------------------------------------------------------------

		// On client side
		[Client]
		void Shoot ()
		{
			RaycastHit hit;
			bool intersect = Physics.Raycast (CameraPosition (), CameraDirection (), out hit, oponentMask);

			if (intersect && hit.collider.tag == PLAYER_TAG)
				CmdDamageToOponent (hit.collider.name, currentWeapon.damage);
		}

		// On server side
		[Command]
		void CmdDamageToOponent (string playerId, float damage)
		{
			Debug.Log (playerId + " has been shot!");
			Player oponentPlayer = GameManager.GetPlayer (playerId);

			oponentPlayer.RpcTakeDamage (damage);
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

		private PlayerWeapon currentWeapon;

		private WeaponManager weaponManager;
	}
}