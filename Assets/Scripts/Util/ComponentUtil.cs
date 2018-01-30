using System;
using UnityEngine;

namespace Util
{
    public class ComponentUtil
    {
        public delegate void TryGetBlock<COMPONENT>(COMPONENT component);

        public static void tryGet<COMPONENT>(GameObject gameObject, TryGetBlock<COMPONENT> block)
        {
            if (gameObject == null) return;

            var nestedComponent = gameObject.GetComponent<COMPONENT>();
            if (nestedComponent == null) return;

            block(nestedComponent);
        }

        public static void tryGet<COMPONENT>(MonoBehaviour behaviour, TryGetBlock<COMPONENT> block)
        {
            tryGet(behaviour.gameObject, block);
        }
    }
}