using Fps.Weapon.State;
using UnityEngine;

namespace Fps.Weapon
{
    public class LoadedWeaponState : WeaponState
    {
        //-----------------------------------------------------------------------------
        // Public Methods
        //-----------------------------------------------------------------------------

        public override bool Shoot(Transform origin, out RaycastHit target, LayerMask targetMask)
        {
			Debug.LogFormat("{0} shoot bullet {1}", weapon, ++shotCounter);
            return weapon.ShootAction(origin, out target, targetMask);
        }

        public override void PlayShootEffect()
        {
            --remainAmmo;
            if (remainAmmo == 0)
                weapon.GoToUnloadedState();
            weapon.PlayShootEffectAction();
        }

        public override void HitTarget(GameObject gameObject, float distance, Vector3 position, Vector3 normal)
        {
            weapon.HitTargetAction(gameObject, distance, position, normal);
        }

        public override void Reload()
        {
            weapon.GoToLoadingState();
        }

        public override string ToString()
        {
            return "Loaded";
        }

        //-----------------------------------------------------------------------------
        // Properties
        //-----------------------------------------------------------------------------

		public override int RemainAmmo {
			get { return remainAmmo; }
		}

        //-----------------------------------------------------------------------------
        // Attributes
        //-----------------------------------------------------------------------------

        private readonly RechargeableWeapon weapon;

        private int remainAmmo;

        private int shotCounter;

        //-----------------------------------------------------------------------------
        // Constructors
        //-----------------------------------------------------------------------------

        public LoadedWeaponState(RechargeableWeapon weapon, int maxAmmo)
        {
            this.weapon = weapon;
            remainAmmo = maxAmmo;
            shotCounter = 0;
            Debug.LogFormat("{0} loaded with {1} bullets", weapon, RemainAmmo);
        }
    }
}