using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

namespace Util
{
    public class LocalPayerAccessor : MonoBehaviour
    {
        public GameObject GameObject
        {
            get { return _gameObject == null ? _gameObject = Find() : _gameObject; }
        }

        public T GetComponent<T>()
        {
            return GameObject.GetComponent<T>();
        }
        
        #region Private Merthods

        GameObject Find()
        {
            return GameObject.FindGameObjectsWithTag(tag)
                .First(IsLocalPlayer());
        }

        static Func<GameObject, bool> IsLocalPlayer()
        {
            return it => it.GetComponent<NetworkIdentity>().isLocalPlayer;
        }

        #endregion
        
        #region Attributes
        
        GameObject _gameObject;

        [SerializeField] string tag = "Player";
        
        #endregion
    }
}