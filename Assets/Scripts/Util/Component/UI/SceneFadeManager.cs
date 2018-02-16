using UnityEngine;
using UnityEngine.SceneManagement;

namespace Util.Component.UI
{
    [RequireComponent(typeof(FadeEffect))]
    public class SceneFadeManager : MonoBehaviour
    {
        public float FadeOut()
        {
            return FadeEffect.Out();
        }
        
        // OnLevelWasLoaded is called when a level is loaded.
        // It takes loaded level index (int) as a parameter so you can limit the fade in to certain scenes.
        void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
            FadeEffect.In();
        }

        void OnEnable() {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        void OnDisable() {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private FadeEffect FadeEffect
        {
            get { return GetComponent<FadeEffect>(); }
        }
       
        void Awake()
        {
            if (Instance == null)
                Instance = this;
            else {
                Destroy(gameObject);
            }

            // Sets this to not be destroyed when reloading scene.
            DontDestroyOnLoad(gameObject);            
        }

        public static SceneFadeManager Instance { get; private set; }
    }
}