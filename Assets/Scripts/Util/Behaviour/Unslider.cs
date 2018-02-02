using UnityEngine;

namespace Util.Behaviours
{
    public class Unslider : MonoBehaviour
    {
        private void Update()
        {
            RaycastHit hit;
            var ray = new Ray(transform.position, -transform.up);
            if (!Physics.Raycast(ray, out hit, raycastLength) || Tag(hit) != surfaceTag || Rigidbody(hit) == null) return;

            Rigidbody(hit).isKinematic = true;
        }

        #region private Methods
        
        private static string Tag(RaycastHit hit)
        {
            return hit.collider.tag;
        }

        private static Rigidbody Rigidbody(RaycastHit hit)
        {
            return hit.collider.gameObject.GetComponent<Rigidbody>();
        }
        
        #endregion

        #region  Attributes

        [SerializeField] private string surfaceTag = "Floor";

        [SerializeField] private float raycastLength = 0.5f;

        #endregion
    }
}