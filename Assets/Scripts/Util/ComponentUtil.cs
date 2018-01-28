using UnityEngine;

namespace Util
{
    public class ComponentUtil
    {
        public delegate void TryGetBlock<COMPONENT>(COMPONENT component);
        
        public static void tryGet<COMPONENT>(MonoBehaviour behaviour, TryGetBlock<COMPONENT> block)
        {
            tryGet(behaviour.gameObject, block);
        }
        
        public static void tryGet<COMPONENT>(GameObject gameObject, TryGetBlock<COMPONENT> block)
        {
            COMPONENT nestedComponent = gameObject.GetComponent<COMPONENT>();
            if (nestedComponent != null) block(nestedComponent);
        }
    }
}