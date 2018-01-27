using UnityEngine;

namespace Util
{
    public class DestroyTimer : MonoBehaviour
    {
        public float lifetime = 2;
     
        void Start ()
        {
            DestroyChildren();
            Destroy (gameObject, lifetime);
        }

        private void DestroyChildren()
        {
            foreach (Transform child in gameObject.transform)
                Destroy(child.gameObject, lifetime);
        }
    }
}

