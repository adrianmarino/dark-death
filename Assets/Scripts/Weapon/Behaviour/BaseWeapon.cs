using System.Collections;
using System.Xml;
using UnityEngine;
using Fps.Weapon.State;
using Fps.Weapon.Animation;
using Util;

namespace Fps.Weapon
{
    [RequireComponent(typeof(WeaponRecoilAnimation))]
    [RequireComponent(typeof(WeaponSwayAnimation))]
    public abstract class BaseWeapon : MonoBehaviour, IWeapon
    {
        //-----------------------------------------------------------------------------
        // Public Methods
        //-----------------------------------------------------------------------------

        private IEnumerator AsyncHitEffect(GameObject target, float distance, Vector3 position, Vector3 normal)
        {
            GameObject _hitEffect = Instantiate(
                HitEffect,
                position,
                Quaternion.LookRotation(normal)
            );
            Destroy(_hitEffect, 2f);
            
            target.GetComponent<HittableObject>()?.Hit(normal, distance, impactForce);

            yield return null;
        }

        public void HitTargetAction(GameObject target, float distance, Vector3 position, Vector3 normal)
        {
            StartCoroutine(AsyncHitEffect(target, distance, position, normal));
        }

        public virtual bool Shoot(Transform origin, out RaycastHit target, LayerMask targetMask)
        {
            return State.Shoot(origin, out target, targetMask);
        }

        public virtual void HitTarget(GameObject target, float distance, Vector3 position, Vector3 normal)
        {
            State.HitTarget(target, distance, position, normal);
        }

        public virtual void PlayShootEffect()
        {
            State.PlayShootEffect();
        }

        public virtual void Reload()
        {
            State.Reload();
        }

        public override string ToString()
        {
            return Name;
        }

        public void Hide()
        {
            model.SetActive(false);
        }

        public void Show()
        {
            model.SetActive(true);
        }

        public bool ShootAction(Transform origin, out RaycastHit target, LayerMask targetMask)
        {
            return Physics.Raycast(
                origin.position,
                origin.forward,
                out target,
                targetMask
            );
        }

        public void PlayShootEffectAction()
        {
            ShootSound.Play();
            muzzleFlash.Stop();
            muzzleFlash.Play();
            shootSmoke.Play();
            WeaponRecoilAnimation().Play();
        }

        //-----------------------------------------------------------------------------
        // Protected Methods
        //-----------------------------------------------------------------------------

        protected abstract WeaponState InitState();

        //-----------------------------------------------------------------------------
        // Properties
        //-----------------------------------------------------------------------------

        public string Name
        {
            get { return _name; }
        }

        public WeaponState State
        {
            get
            {
                if (currentState == null)
                    currentState = InitState();
                return currentState;
            }
            set { currentState = value; }
        }

        public float Damage
        {
            get { return damage; }
        }

        public float Range
        {
            get { return range; }
        }

        public float FireRate
        {
            get { return fireRate; }
        }

        public GameObject GameObject
        {
            get { return this.gameObject; }
        }

        AudioSource ShootSound
        {
            get { return GetComponents<AudioSource>()[0]; }
        }

        WeaponRecoilAnimation WeaponRecoilAnimation()
        {
            return GetComponent<WeaponRecoilAnimation>();
        }

        //-----------------------------------------------------------------------------
        // Properties
        //-----------------------------------------------------------------------------

        public int RemainAmmo
        {
            get { return State.RemainAmmo; }
        }

        GameObject HitEffect
        {
            get { return hitEffect; }
        }

        //-----------------------------------------------------------------------------
        // Attributes
        //-----------------------------------------------------------------------------

        [SerializeField] private string _name;

        [SerializeField] private GameObject model;

        [SerializeField] private float damage = 25f;

        [SerializeField] private float range = 100f;

        [SerializeField] private float fireRate = 2f;

        [SerializeField] [Range(300, 1000)] private float impactForce = 300f;

        [SerializeField] private ParticleSystem muzzleFlash;

        [SerializeField] private ParticleSystem shootSmoke;

        [SerializeField] private GameObject hitEffect;

        private WeaponState currentState;

        //-----------------------------------------------------------------------------
        // Constructors
        //-----------------------------------------------------------------------------

        protected BaseWeapon()
        {
            _name = GetType().Name;
        }
    }
}