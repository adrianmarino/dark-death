using UnityEngine;

namespace Util.Behaviours
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(Rigidbody))]
    public class RandomBoxSettings : MonoBehaviour
    {
        private void Awake()
        {
            mass = randomMass ? newMass() : MIN_MASS;
            size = randomSize ? newSize() : MIN_SIZE;
        }

        private void Update()
        {
            ComponentUtil.tryGet<Rigidbody>(this, it => it.mass = mass);
            transform.localScale = new Vector3(size, size, size); 
        }

        private static float newSize()
        {
            return Random.Range(MIN_SIZE, MAX_SIZE);
        }

        private static float newMass()
        {
            return Random.Range(MIN_MASS, MAX_MASS);
        }

        private const float MIN_SIZE = 0.5f;

        private const float MAX_SIZE = 1.2f;

        private const float MIN_MASS = 1;

        private const float MAX_MASS = 15;

        #region Attributes

        [SerializeField] [Range(MIN_MASS, MAX_MASS)] private float mass;

        [SerializeField] private bool randomMass = true;
        
        [SerializeField] [Range(MIN_SIZE, MAX_SIZE)] private float size;

        [SerializeField] private bool randomSize = true;

        #endregion
    }
}