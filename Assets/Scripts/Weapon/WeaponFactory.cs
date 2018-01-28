using UnityEngine;

namespace Fps.Weapon
{
    public class WeaponFactory : MonoBehaviour
    {
        public GameObject InstantiateOnHolder(GameObject weapon, Transform holder)
        {
            GameObject instance = Instantiate(
                weapon,
                holder.position,
                holder.rotation
            );
            instance.transform.SetParent(holder);
            instance.SetActive(false);
            return instance;
        }
    }
}