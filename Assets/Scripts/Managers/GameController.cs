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
        #region Properties
        private static GameController instance;
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
        #endregion

        #region Feilds
        public AudioClip MenuAudio;
		public AudioClip[] Playlist;
        public int LevelToClip;

        private AudioSource _audioSource;
        private bool _inMenu = true;
        private int _audioItr = 1;
        #endregion


        void Awake() {
            _audioSource = GetComponent<AudioSource>();
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
            Debug.Log(string.Format("changing screens to {0}",screenName));
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

        #region Audio management
        public void adjustMasterVolume(float volume) {
            AudioListener.volume = volume;
        }

        private void ChangeAudio(string screenTarget){
            if (!(screenTarget.Equals("AvatarSelectScreen") || screenTarget.Equals("LevelSelectScreen") || screenTarget.Equals("OptionsScreen"))) {
                _inMenu = false;
            } else {
                _inMenu = true;
                return;
            }
            Debug.Log(string.Format("Change Audio called {0}",screenTarget));
			AudioClip soundToSwitchTo;
			if (!_inMenu) {
                Debug.Log("entering active level");
                int soundIndex = _audioItr / LevelToClip;
                if (_audioItr % LevelToClip == 0) {
                    soundIndex--;
                }
                if (soundIndex > Playlist.Length) {
                    _audioItr = 1;
                    soundIndex = 0;
                }
				Debug.Log ("Requested audio change to " + screenTarget);
                soundToSwitchTo = Playlist[soundIndex];
                _audioItr++;
            } else { 
                soundToSwitchTo = MenuAudio;
            }

            _audioSource.clip = soundToSwitchTo;
            _audioSource.Play();
            /*.PlayClipAtPoint(soundToSwitchTo, transform.position);*/
        }
		#endregion

        #region Helper methods
        #endregion
    }
}