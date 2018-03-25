using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

namespace Util
{
    public class LocalPayerAccessor : MonoBehaviour
    {
        public new T GetComponent<T>()
        {
            return GameObject.GetComponent<T>();
        }
     
        private GameObject GameObject
        {
            get { return playerGameObject == null ? playerGameObject = Find() : playerGameObject; }
        }
   
        #region Private Merthods

        private GameObject Find()
        {
            return GameObject.FindGameObjectsWithTag(playerTag).First(IsLocalPlayer());
        }

        private static Func<GameObject, bool> IsLocalPlayer()
        {
            return it => it.GetComponent<NetworkIdentity>().isLocalPlayer;
        }

        #endregion
        
        #region Attributes
        
        GameObject playerGameObject;

        [SerializeField] private string playerTag = "Player";
        
        #endregion
    }
}
