using UnityEngine;
using UnityEngine.Networking;

namespace Fps
{
	[RequireComponent (typeof(WeaponManager))]
	public class PlayerShoot : NetworkBehaviour
	{
		//-----------------------------------------------------------------------------
		// Events
		//-----------------------------------------------------------------------------

		void Update ()
		{
			if (WeaponManager.isReady ())
				UpdateShoot ();
		}

		// On server side
		[Command]
		void CmdDamageToOponent (string playerId, float damage)
		{
			Debug.Log (playerId + " has been shot!");
			Player oponentPlayer = GameManager.GetPlayer (playerId);
			oponentPlayer.RpcTakeDamage (damage);
		}

		// Invoked on the server when hit something...
		[Command]
		void CmdOnHit (Vector3 position, Vector3 normal)
		{
			RpcDoHitEffect (position, normal);
		}

		// Invoked on all client to spawn hit effects...
		[ClientRpc]
		void RpcDoHitEffect (Vector3 position, Vector3 normal)
		{
			Weapon.HitTarget (position, normal);
		}

		[Command]
		void CmdOnShoot ()
		{
			RpcDoShootEffect ();
		}

		// Invoke DoShootEffect command on all client from server side...
		[ClientRpc]
		void RpcDoShootEffect ()
		{
			Weapon.PlayShootEffect ();
		}

		//-----------------------------------------------------------------------------
		// Private Methods
		//-----------------------------------------------------------------------------

		// On client side
		[Client]
		void Shoot ()
		{
			if (!isLocalPlayer)
				return;

			// Invoke OnShoot command on server side...
			Debug.Log (Weapon + " shoot count: " + ++counter);
			CmdOnShoot ();

			RaycastHit target;
			if (Weapon.Shoot (_camera.transform, out target, oponentMask)) {
				if (IsPlayer (target))
					CmdDamageToOponent (target.collider.name, Weapon.Damage);

				// When hit somthing, invoke OnHit on server side... 
				CmdOnHit (target.point, target.normal);
			}
		}

		void UpdateShoot ()
		{
			if (Weapon.FireRate <= 0f) {
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
			InvokeRepeating (SHOOT_METHOD_NAME, 0f, 1f / Weapon.FireRate);
		}

		void EndShoot ()
		{
			CancelInvoke (SHOOT_METHOD_NAME);
		}

		bool IsPlayer (RaycastHit target)
		{
			return target.collider.tag == PLAYER_TAG;
		}

		//-----------------------------------------------------------------------------
		// Properties
		//-----------------------------------------------------------------------------

		Weapon Weapon {
			get { return WeaponManager.CurrentWeapon; }
		}

		WeaponManager WeaponManager {
			get { return GetComponent<WeaponManager> (); }
		}

		GameManager GameManager {
			get { return GameManager.singleton; }
		}

		//-----------------------------------------------------------------------------
		// Constants
		//-----------------------------------------------------------------------------

		private const string PLAYER_TAG = "Player";

		const string SHOOT_METHOD_NAME = "Shoot";

		//-----------------------------------------------------------------------------
		// Attributes
		//-----------------------------------------------------------------------------

		[SerializeField]
		private LayerMask oponentMask;

		[SerializeField]
		private Camera _camera;

		private float counter = 0;
	}
}