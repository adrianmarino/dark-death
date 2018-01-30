using UnityEngine;

namespace Fps.Weapon
{
    public class Unslider : MonoBehaviour
    {
        private void Update()
        {
            RaycastHit hit;
            var ray = new Ray(transform.position, -transform.up);
            if (!Physics.Raycast(ray, out hit, 10) || Tag(hit) != surfaceTag || Rigidbody(hit) == null) return;

            Rigidbody(hit).isKinematic = true;
        }

        private static string Tag(RaycastHit hit)
        {
            return hit.collider.tag;
        }

        private static Rigidbody Rigidbody(RaycastHit hit)
        {
            return hit.collider.gameObject.GetComponent<Rigidbody>();
        }

        [SerializeField] private string surfaceTag = "Floor";
    }
}