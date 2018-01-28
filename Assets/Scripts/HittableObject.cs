using UnityEngine;
using UnityEngine.Networking;

namespace Fps
{
    [RequireComponent(typeof(NetworkIdentity))]
    public class HittableObject : NetworkBehaviour
    {
        //-----------------------------------------------------------------------------
        // Public Methods
        //-----------------------------------------------------------------------------

        public void Hit(Vector3 normal, float impactForce)
        {
            Rigidbody().AddForce(-normal * impactForce);
        }

        //-----------------------------------------------------------------------------
        // Private Methods
        //-----------------------------------------------------------------------------


        private Rigidbody Rigidbody()
        {
            return gameObject.GetComponent<Rigidbody>();
        }

        //-----------------------------------------------------------------------------
        // Constants
        //-----------------------------------------------------------------------------

        private static readonly int MIN_WEIGHT = 1;

        private static readonly int MAX_WEIGHT = 20;

        //-----------------------------------------------------------------------------
        // Attributes
        //-----------------------------------------------------------------------------

        [SerializeField] private float weight;

        //-----------------------------------------------------------------------------
        // Constructors
        //-----------------------------------------------------------------------------

        public HittableObject()
        {
            weight = Util.Random.GetNumber(MIN_WEIGHT, 20);
        }
    }
}