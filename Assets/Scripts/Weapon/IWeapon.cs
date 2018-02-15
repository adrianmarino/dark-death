using Fps.Player;
using UnityEngine;
using Fps.Weapon.State;

namespace Fps.Weapon
{
    public interface IWeapon
    {
        //-----------------------------------------------------------------------------
        // Public Methods
        //-----------------------------------------------------------------------------

        IPausable[] Pausables();
        
        bool Shoot(Transform origin, out RaycastHit target, LayerMask targetMask);

        void HitTarget(GameObject target, float distance, Vector3 position, Vector3 normal);

        void PlayShootEffect();

        void Hide();

        void Show();

        void Reload();

        //-----------------------------------------------------------------------------
        // Properties
        //-----------------------------------------------------------------------------

        int RemainAmmo { get; }

        GameObject GameObject { get; }

        float Damage { get; }

        float Range { get; }

        float FireRate { get; }

        string Name { get; }

        WeaponState State { get; }
    }
}