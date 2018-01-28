using System;
using UnityEngine;
using UnityEngine.Networking;
using Util;

namespace Fps
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

        //-----------------------------------------------------------------------------
        // Private Methods
        //-----------------------------------------------------------------------------

        private void log(float distance, float impactForce, float force)
        {
            Debug.LogFormat("Hit %s appling a force of %f (Force:%f/Distance:%f)", name, impactForce, force, distance);
        }

        private float calculateImpactForce(double distance, double originalImpactForce)
        {
            return (float) (originalImpactForce / distance);
        }
    }
}