using System;
using UnityEngine;
using UnityEngine.Networking;

namespace Util.Behaviours
{
    [RequireComponent(typeof(NetworkIdentity))]
    [RequireComponent(typeof(Rigidbody))]
    public class HittableObject : NetworkBehaviour
    {
        public void Hit(Vector3 normal, float distance, float impactForce)
        {
            ComponentUtil.tryGet<Rigidbody>(this, it =>
            {
                it.isKinematic = false;
                var force = calculateImpactForce(distance, impactForce);
                it.AddForce(-normal * force);
                log(distance, impactForce, force);
            });
        }

        private void OnCollisionExit(Collision other)
        {
            ComponentUtil.tryGet<Rigidbody>(this, it => it.isKinematic = false);
        }
        
        private float calculateImpactForce(double distance, double originalImpactForce)
        {
            return (float) (originalImpactForce / distance);
        }

        private void log(float distance, float impactForce, float force)
        {
            Debug.LogFormat(
                "Hit {0} appling a force of {1} (Force:{2}/Distance:{3})",
                name,
                Math.Round(impactForce, 2),
                Math.Round(force, 2),
                Math.Round(distance, 2)
            );
        }
    }
}