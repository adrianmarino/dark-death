using UnityEngine;
using UnityEngine.Networking;

namespace Fps.Player
{
    public class PlayerShootWeaponAction : PlayerWeaponAction
    {
        //-----------------------------------------------------------------------------
        // Event Methods
        //-----------------------------------------------------------------------------

        void Update()
        {
            if (!WeaponManager.isReady() || Pause) return;
            UpdateShoot();
        }

        // On server side
        [Command]
        void CmdDamageToOponent(string playerId, float damage)
        {
            Debug.Log(playerId + " has been shot!");
            PlayerState oponentPlayer = GameManager.GetPlayer(playerId);
            oponentPlayer.RpcTakeDamage(damage);
        }

        // Invoked on the server when hit something...
        [Command]
        void CmdOnHit(GameObject target, float distance, Vector3 position, Vector3 normal)
        {
            RpcDoHitEffect(target, distance, position, normal);
        }

        // Invoked on all client to spawn hit effects...
        [ClientRpc]
        void RpcDoHitEffect(GameObject target, float distance, Vector3 position, Vector3 normal)
        {
            Weapon.HitTarget(target, distance, position, normal);
        }

        [Command]
        void CmdOnShoot()
        {
            RpcDoShootEffect();
        }

        // Invoke DoShootEffect command on all client from server side...
        [ClientRpc]
        void RpcDoShootEffect()
        {
            Weapon.PlayShootEffect();
        }

        //-----------------------------------------------------------------------------
        // Private Methods
        //-----------------------------------------------------------------------------

        // On client side
        [Client]
        void Shoot()
        {
            if (!isLocalPlayer) return;

            // Invoke OnShoot command on server side...
            CmdOnShoot();

            RaycastHit target;
            if (Weapon.Shoot(_camera.transform, out target, oponentMask))
            {
                Collider targetCollider = target.collider;

                if (IsPlayer(target))
                    CmdDamageToOponent(targetCollider.name, Weapon.Damage);

                // When hit somthing, invoke OnHit on server side...
                CmdOnHit(targetCollider.gameObject, target.distance, target.point, target.normal);
            }
        }

        void UpdateShoot()
        {
            if (Weapon.FireRate <= 0f)
            {
                if (Util.Input.GetFireButtonDown())
                    Shoot();
            }
            else
            {
                if (Util.Input.GetFireButtonDown())
                    BurstShoot();
                else if (Util.Input.GetFireButtonUp())
                    EndShoot();
            }

            UpdateAmmoPanel();
        }

        void BurstShoot()
        {
            InvokeRepeating(SHOOT_METHOD_NAME, 0f, 1f / Weapon.FireRate);
        }

        void EndShoot()
        {
            CancelInvoke(SHOOT_METHOD_NAME);
        }

        bool IsPlayer(RaycastHit target)
        {
            return target.collider.tag == PLAYER_TAG;
        }

        //-----------------------------------------------------------------------------
        // Properties
        //-----------------------------------------------------------------------------

        GameManager GameManager
        {
            get { return GameManager.Instance; }
        }

        //-----------------------------------------------------------------------------
        // Constants
        //-----------------------------------------------------------------------------

        private const string PLAYER_TAG = "Player";

        const string SHOOT_METHOD_NAME = "Shoot";

        //-----------------------------------------------------------------------------
        // Attributes
        //-----------------------------------------------------------------------------

        [SerializeField] private LayerMask oponentMask;

        [SerializeField] private Camera _camera;
    }
}