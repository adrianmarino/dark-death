using UnityEngine;
using UnityEngine.SceneManagement;
using Util.Component.UI.Fade;

namespace Util.Component.UI
{
    [RequireComponent(typeof(FadeEffect))]
    public class SceneFadeTransission : MonoBehaviour
    {        
        public void FadeIn()
        {
            effect = new FadeOutEffect(texture, speed, drawDepth);
        }

        public void FadeOut()
        {
            effect = new FadeInEffect(texture, speed, drawDepth);
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
            FadeIn();
        }

        void OnGUI()
        {
            while(!effect.doesFinish()) effect.NextStep();
        }
        
        #region OnSceneLoaded Impl
        
        void OnEnable() {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        void OnDisable() {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
        
        #endregion

        #region Singleton Implementation
       
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

        public static SceneFadeTransission Instance { get; private set; }

        public static bool IsReady {
            get
            {
                return Instance != null;
            }
        }

        #endregion

        #region Attributes

        [SerializeField] Texture2D texture;

        [SerializeField] float speed = 0.8f;

        [SerializeField] int drawDepth = -1000;

        FadeEffect effect;

        #endregion
    }
}