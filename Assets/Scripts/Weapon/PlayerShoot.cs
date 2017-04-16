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
			UpdateShoot ();
		}

		void Start ()
		{
			weaponManager = GetComponent<WeaponManager> ();
		}

		//-----------------------------------------------------------------------------
		// Private Methods
		//-----------------------------------------------------------------------------

		[Command]
		void CmdOnShoot ()
		{
			RpcDoShootEffect ();
		}

		// Invoke DoShootEffect command on all client from server side...
		[ClientRpc]
		void RpcDoShootEffect ()
		{
			weaponManager.GetCurrentWeaponGraphics ().muzzleFlash.Play ();
		}

		// On client side
		[Client]
		void Shoot ()
		{
			if (!isLocalPlayer)
				return;

			// Invoke OnShoot command on server side...
			CmdOnShoot ();

			Debug.Log ("SHOOT: " + ++counter);
			RaycastHit hit;
			bool intersect = Physics.Raycast (CameraPosition (), CameraDirection (), out hit, oponentMask);

			if (intersect && hit.collider.tag == PLAYER_TAG)
				CmdDamageToOponent (hit.collider.name, currentWeapon.damage);
		}

		void UpdateShoot ()
		{
			if (currentWeapon.fireRate <= 0f) {
				if (Util.Input.GetFireButtonDown ())
					Shoot ();
			} else {
				if (Util.Input.GetFireButtonDown ())
					BurstShoot ();
				else if (Util.Input.GetFireButtonUp ())
					EndShoot ();
			}
		}

		void BurstShoot ()
		{
			InvokeRepeating (SHOOT_METHOD_NAME, 0f, 1f / currentWeapon.fireRate);
		}

		void EndShoot ()
		{
			CancelInvoke (SHOOT_METHOD_NAME);
		}

		// On server side
		[Command]
		void CmdDamageToOponent (string playerId, float damage)
		{
			Debug.Log (playerId + " has been shot!");
			Player oponentPlayer = GameManager.singleton.GetPlayer (playerId);

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

		const string SHOOT_METHOD_NAME = "Shoot";

		//-----------------------------------------------------------------------------
		// Attributes
		//-----------------------------------------------------------------------------

		private float counter = 0;

		[SerializeField]
		private LayerMask oponentMask;

		[SerializeField]
		private Camera _camera;

		private PlayerWeapon currentWeapon;

		private WeaponManager weaponManager;
	}
}