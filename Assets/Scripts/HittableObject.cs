using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Util;

namespace Fps
{
    [RequireComponent(typeof(NetworkIdentity))]
    [RequireComponent(typeof(Rigidbody))]
    public class HittableObject : NetworkBehaviour
    {
        //-----------------------------------------------------------------------------
        // Public Methods
        //-----------------------------------------------------------------------------

        public void Hit(Vector3 normal, float distance, float impactForce)
        {
            StartCoroutine(AsyncHit(normal, distance, impactForce));
        }

        //-----------------------------------------------------------------------------
        // Private Methods
        //-----------------------------------------------------------------------------

        IEnumerator AsyncHit(Vector3 normal, float distance, float impactForce)
        {
            ComponentUtil.tryGet<Rigidbody>(this, it =>
            {
                var force = calculateImpactForce(distance, impactForce);
                Debug.Log($"Hit {name} appling a force of {force} (Force:{impactForce}/Distance:{distance})");
                it.AddForce(-normal * force);
            });
            yield return null;
        }

        private float calculateImpactForce(double distance, double originalImpactForce)
        {
            return (float) (originalImpactForce / distance);
        }
    }
}