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
        private static GameController instance;

		public AudioClip menuAudio;
		public AudioClip level1Audio;

        public static GameController Instance
        {
            get
            {
                if (instance == null) {
                    instance = new GameController();
                }
                return instance;
            }
            private set
            {
                instance = value;
            }
        }
        

        void Awake() {
			Debug.Log ("Game Controller awake() invoked.");
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
            Debug.Log("changing screens");
			ChangeAudio (screenName);
            SceneManager.LoadScene(screenName, LoadSceneMode.Single);
        }

        public void loadScreenAdditive(string screenName) {
			ChangeAudio (screenName);
            SceneManager.LoadScene(screenName, LoadSceneMode.Additive);
        }

        public void restartCurrentScene() {
            int scene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(scene, LoadSceneMode.Single);
        }
        #endregion

		#region Volume management
		public void adjustMasterVolume(float volume){
			AudioListener.volume = volume;
		}
		#endregion

		#region Audio management
		private void ChangeAudio(string screenTarget){
			if (screenTarget.Equals ("AvatarSelectScreen") || screenTarget.Equals ("LevelSelectScreen") || screenTarget.Equals ("OptionsScreen")) {
				return;
			}

			AudioClip soundToSwitchTo;
			if (screenTarget.Equals ("level1")) {
				Debug.Log ("Requested audio change to " + screenTarget);
				soundToSwitchTo = level1Audio;
			} else {
				soundToSwitchTo = menuAudio;
			}

			AudioSource.PlayClipAtPoint (soundToSwitchTo, transform.position);
		}
		#endregion

        #region Helper methods
        #endregion
    }
}