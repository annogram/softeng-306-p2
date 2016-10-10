using UnityEngine;
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

        // Update is called once per frame
        void Update() {
        }

        #region Helper methods
        #endregion
    }
}