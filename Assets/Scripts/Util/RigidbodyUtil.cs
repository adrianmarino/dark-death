using UnityEngine;

namespace Util
{
    public class RigidbodyUtil
    {
        public static void AddForce(Rigidbody rigidbody, Vector3 force, ForceMode forceMode)
        {
            if (force == Vector3.zero)
                return;
            rigidbody.AddForce(force * Time.fixedDeltaTime, forceMode);
        }

        public static void Move(Rigidbody rigidbody, Vector3 velocity)
        {
            if (velocity == Vector3.zero) return;
            rigidbody.MovePosition(rigidbody.position + velocity * Time.fixedDeltaTime);
        }

        public static void Rotate(Rigidbody rigidbody, Vector3 rotation)
        {
            if (rotation == Vector3.zero)
                return;
            rigidbody.MoveRotation(rigidbody.rotation * Quaternion.Euler(rotation));
        }

        //-----------------------------------------------------------------------------
        // Constructors
        //-----------------------------------------------------------------------------

        private RigidbodyUtil()
        {
        }
    }
}