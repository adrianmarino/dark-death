using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.Linq;
using Util;

namespace Fps.Weapon
{
    [RequireComponent(typeof(WeaponFactory))]
    public class WeaponManager : NetworkBehaviour
    {
        //-----------------------------------------------------------------------------
        // Event Methods
        //-----------------------------------------------------------------------------

        void Start()
        {
            // Workaround by unity 
            if (weaponPrefabs.Count > 0)
            {
                UseWeapon(defaultWeapon);

                if (isLocalPlayer)
                    CmdOnUseWeapon(defaultWeapon);
            }
        }

        [Client]
        void Update()
        {
            if (!isLocalPlayer)
                return;

            if (CurrentWeapon.State is LoadingWeaponState)
                return;

            if (Util.Input.NextWeaponButton())
            {
                int weaponNumber = Weapons.NextPosition(currentWeapon);
                UseWeapon(weaponNumber);
                CmdOnUseWeapon(weaponNumber);
            }
            else if (Util.Input.PreviousWeaponButton())
            {
                int weaponNumber = Weapons.PreviousPosition(currentWeapon);
                UseWeapon(weaponNumber);
                CmdOnUseWeapon(weaponNumber);
            }
        }

        //-----------------------------------------------------------------------------
        // Public Methods
        //-----------------------------------------------------------------------------

        public bool isReady()
        {
            return CurrentWeapon != null; // Workaround by unity bug.
        }

        public int RemainAmmo()
        {
            return CurrentWeapon.RemainAmmo;
        }

        public static WeaponManager GetFromPlayer()
        {
            GameObject player = GameObject.Find("Player");
            if (player == null)
            {
                Debug.Log("Not found a player in scene!");
                return null;
            }

            WeaponManager weaponManager = player.GetComponent<WeaponManager>();
            if (weaponManager == null)
            {
                Debug.Log("Not found weapon manager componente in player object!");
                return null;
            }

            return weaponManager;
        }

        //-----------------------------------------------------------------------------
        // Private Methods
        //-----------------------------------------------------------------------------

        GameObject CreateIntoHolder(GameObject _weaponPrefab)
        {
            GameObject weapon = Factory.InstantiateOnHolder(_weaponPrefab, weaponHolder);

            if (isLocalPlayer)
                Util.Layer.SetLayerRecursively(weapon, LayerMask.NameToLayer(weaponLayerName));

            return weapon;
        }

        void UseWeapon(int weaponNumber)
        {
            if (currentWeapon != null)
                currentWeapon.SetActive(false);

            currentWeapon = Weapons[weaponNumber];
            currentWeapon.SetActive(true);
        }

        [Command]
        void CmdOnUseWeapon(int weaponNumber)
        {
            RpcDoUseWeapon(weaponNumber);
        }

        [ClientRpc]
        void RpcDoUseWeapon(int weaponNumber)
        {
            UseWeapon(weaponNumber);
        }

        //-----------------------------------------------------------------------------
        // Properties
        //-----------------------------------------------------------------------------

        public List<GameObject> Weapons
        {
            get
            {
                if (weapons == null)
                    weapons = weaponPrefabs.Select(CreateIntoHolder).ToList();
                return weapons;
            }
        }

        public IWeapon CurrentWeapon
        {
            get { return currentWeapon.GetComponent<IWeapon>(); }
        }

        WeaponFactory Factory
        {
            get { return GetComponent<WeaponFactory>(); }
        }

        //-----------------------------------------------------------------------------
        // Attributes
        //-----------------------------------------------------------------------------

        [SerializeField] private string weaponLayerName = "Weapon";

        [SerializeField] private Transform weaponHolder;

        [SerializeField] private int defaultWeapon = 1;

        [SerializeField] private List<GameObject> weaponPrefabs;

        private List<GameObject> weapons;

        private GameObject currentWeapon;
    }
}