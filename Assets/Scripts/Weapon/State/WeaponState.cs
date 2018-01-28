using UnityEngine;

namespace Fps.Weapon.State
{
    public abstract class WeaponState
    {
        //-----------------------------------------------------------------------------
        // Public Methods
        //-----------------------------------------------------------------------------

        public virtual bool Shoot(Transform origin, out RaycastHit target, LayerMask targetMask)
        {
            target = new RaycastHit();
            return false;
        }

        public virtual void HitTarget(GameObject gameObject, float distance, Vector3 position, Vector3 normal)
        {
        }

        public virtual void PlayShootEffect()
        {
        }

        public virtual void Reload()
        {
        }

        //-----------------------------------------------------------------------------
        // Properties
        //-----------------------------------------------------------------------------

        public virtual int RemainAmmo
        {
            get { return 0; }
        }
    }
}