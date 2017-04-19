﻿using UnityEngine;
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
			Weapon.PlayShootEffects ();
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
			Weapon.Hit (position, normal);
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
			bool intersect = Physics.Raycast (
				                 CameraPosition (), 
				                 CameraDirection (), 
				                 out hit, 
				                 oponentMask
			                 );
			if (intersect) {
				if (hit.collider.tag == PLAYER_TAG)
					CmdDamageToOponent (hit.collider.name, Weapon.Damage);

				// When hit somthing, invoke OnHit on server side... 
				CmdOnHit (hit.point, hit.normal);
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
		// Properties
		//-----------------------------------------------------------------------------

		Weapon Weapon {
			get { return WeaponManager.CurrentWeapon; }
		}

		WeaponManager WeaponManager {
			get { return GetComponent<WeaponManager> (); }
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
	}
}