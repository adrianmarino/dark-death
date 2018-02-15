using System;
using System.Collections.Generic;
using System.Linq;
using Fps.Weapon;
using ProBuilder2.Common;
using UnityEngine;

namespace Fps.Player
{
    [RequireComponent(typeof(PlayerController))]
    [RequireComponent(typeof(PlayerMotor))]
    [RequireComponent(typeof(PlayerState))]
    [RequireComponent(typeof(PlayerShootWeaponAction))]
    [RequireComponent(typeof(PlayerReloadWeaponAction))]
    [RequireComponent(typeof(WeaponManager))]
    public class PlayerPauseManager : MonoBehaviour
    {
        public void Pause()
        {
            pausablesComponents().ForEach(MakePause(true));
        }
        
        public void Resume()
        {
            pausablesComponents().ForEach(MakePause(false));
        }

        private List<IPausable> pausablesComponents()
        {
            return GetComponents<IPausable>().Concat(CurrentWeapon().Pausables()).ToList();
        }

        private IWeapon CurrentWeapon()
        {
            return GetComponent<WeaponManager>().CurrentWeapon;
        }
        
        private static Action<IPausable> MakePause(bool value)
        {
            return it => it.Pause = value;
        }
    }
}