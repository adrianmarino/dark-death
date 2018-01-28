using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(AudioSource))]
public class Weapon : NetworkBehaviour
{
    //-----------------------------------------------------------------------------
    // Attributes
    //-----------------------------------------------------------------------------

    public void PlayShootEffects()
    {
        ShootSound.Play();
        MuzzleFlash.Play();
    }

    //-----------------------------------------------------------------------------
    // Properties
    //-----------------------------------------------------------------------------

    public string Name
    {
        get { return _name; }
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

    public GameObject Graphics
    {
        get { return this.gameObject; }
    }

    private AudioSource ShootSound
    {
        get { return GetComponent<AudioSource>(); }
    }

    private ParticleSystem MuzzleFlash
    {
        get { return muzzleFlash; }
    }

    public GameObject HitEffect
    {
        get { return hitEffect; }
    }

    //-----------------------------------------------------------------------------
    // Attributes
    //-----------------------------------------------------------------------------

    [SerializeField] private string _name = "HeavyBlaster";

    [SerializeField] private float damage = 25f;

    [SerializeField] private float range = 100f;

    [SerializeField] private float fireRate = 2f;

    [SerializeField] private ParticleSystem muzzleFlash;

    [SerializeField] private GameObject hitEffect;
}