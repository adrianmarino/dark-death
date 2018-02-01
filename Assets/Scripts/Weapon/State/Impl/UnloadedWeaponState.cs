using UnityEngine;
using Fps.Weapon.State;

namespace Fps.Weapon
{
    public class UnloadedWeaponState : WeaponState
    {
        public override bool Shoot(Transform origin, out RaycastHit target, LayerMask targetMask)
        {
            Debug.LogFormat("Try shoot {0} without bullets", weapon);
            return base.Shoot(origin, out target, targetMask);
        }

        public override void Reload()
        {
            weapon.GoToLoadingState();
        }

        public override string ToString()
        {
            return "Unloaded";
        }

        //-----------------------------------------------------------------------------
        // Attributes
        //-----------------------------------------------------------------------------

        private readonly RechargeableWeapon weapon;

        //-----------------------------------------------------------------------------
        // Constructors
        //-----------------------------------------------------------------------------

        public UnloadedWeaponState(RechargeableWeapon weapon)
        {
            this.weapon = weapon;
        }
    }
}