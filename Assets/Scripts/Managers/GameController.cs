using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace Managers {
    /// <summary>
    /// The game controller is a script which manages :
    /// - <list type="responsibilites">
    /// Controlling what music is currently playing
    /// What level is currently loaded
    /// Player save persistence
    /// Including saving settings
    /// Control loading screen and pause screen.
    /// Control non-gameplay button captures.
    ///</list>
    /// </summary>
    public class GameController : MonoBehaviour {
        public static GameController instance { get; private set; }

        void Awake() {
            if (instance == null) {
                instance = this;
            } else if (this != instance) {
                Destroy(gameObject);
            }
            // We dont want the game manager to be destroyed when when we load a new scene since it 
            // is a game object that manages levels
            DontDestroyOnLoad(instance);
        }

        #region Screen management
        public void loadScreenSingle(string screenName) {
            SceneManager.LoadScene(screenName, LoadSceneMode.Single);
        }

        public void loadScreenAdditive(string screenName) {
            SceneManager.LoadScene(screenName, LoadSceneMode.Additive);
        }

        public void restartCurrentScene() {
            int scene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(scene, LoadSceneMode.Single);
        }
        #endregion

        #region Helper methods
        #endregion
    }
}