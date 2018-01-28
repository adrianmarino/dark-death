using UnityEngine;

namespace Fps.Weapon
{
    public class Unslider : MonoBehaviour
    {
        private void Update()
        {
            RaycastHit hit;
            var ray = new Ray(transform.position, -transform.up);
            if (!Physics.Raycast(ray, out hit, 10) || Tag(hit) != surfaceTag) return;

            Rigidbody(hit).isKinematic = true;
            log(hit);
        }

        private static void log(RaycastHit hit)
        {
            Debug.LogFormat("ColliderTag: {}. isKinematic: {}", Tag(hit), IsKinematic(hit));
        }

        private static bool IsKinematic(RaycastHit hit)
        {
            return Rigidbody(hit).isKinematic;
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