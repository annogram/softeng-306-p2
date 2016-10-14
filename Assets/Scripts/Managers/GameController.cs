using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Collections;
using System;

namespace Managers {
    /// <summary>
    /// The game controller is a script which manages :
    /// <list type="responsibilites">
    /// <item>Controlling what music is currently playing</item>
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

        private OptionValues _volume;
        private AudioSource _audioSource;
        private bool _inMenu = true;
        private int _audioTrack = 1;
        private int _tokens = 0;
        #endregion

        #region Constructor
        void Awake() {
            _audioSource = GetComponent<AudioSource>();
            if (instance == null) {
                instance = this;
                // Load persistent values from machine
                this.loadPreferences();
            } else if (this != instance) {
                Destroy(gameObject);
            }
            // We dont want the game manager to be destroyed when when we load a new scene since it 
            // is a game object that manages levels
            DontDestroyOnLoad(instance);
        }
        #endregion

        #region Deconstructor
        void OnDestroy() {
            PlayerPrefs.SetFloat("MasterVolume", _volume.Master);
            PlayerPrefs.SetFloat("MusicVolume", _volume.Music);
            PlayerPrefs.SetInt("Tokens", _tokens);
            PlayerPrefs.Save();
        }
        #endregion

        #region Screen management
        public void loadScreenSingle(string screenName) {
            //Debug.Log(string.Format("changing screens to {0}", screenName));
            ChangeAudio(screenName);
            SceneManager.LoadScene(screenName, LoadSceneMode.Single);
        }

        public void loadScreenAdditive(string screenName) {
            ChangeAudio(screenName);
            SceneManager.LoadScene(screenName, LoadSceneMode.Additive);
        }

        public void restartCurrentScene() {
            int scene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(scene, LoadSceneMode.Single);
        }
        #endregion

        #region Audio management
        internal void adjustMasterVolume(float volume) {
            _volume.Master = volume;
            _audioSource.volume = _volume.Master;
            //PlayerPrefs.SetFloat("MasterVolume", _volume.Master);
            //PlayerPrefs.Save();
        }

        internal void adjustMusicVolume(float volume) {
            _volume.Music = volume;
            AudioListener.volume = _volume.Music;
            //PlayerPrefs.SetFloat("MusicVolume", _volume.Music);
            //PlayerPrefs.Save();
        }

        internal void adjustEffectVolume(float volume) {
            throw new NotImplementedException();
        }

        private void ChangeAudio(string screenTarget) {
            if (!(screenTarget.Equals("AvatarSelectScreen") || screenTarget.Equals("LevelSelectScreen") || screenTarget.Equals("OptionsScreen") || screenTarget.Equals("Start"))) {
                _inMenu = false;
            } else {
                _inMenu = true;
            }
            AudioClip soundToSwitchTo;
            if (!_inMenu) {
                //Debug.Log("entering active level");
                int soundIndex = _audioTrack / LevelToClip;
                if (_audioTrack % LevelToClip == 0) {
                    soundIndex--;
                }
                if (soundIndex > Playlist.Length) {
                    _audioTrack = 1;
                    soundIndex = 0;
                }
                soundToSwitchTo = Playlist[soundIndex];
                _audioTrack++;
            } else {
                soundToSwitchTo = MenuAudio;
            }

            if (soundToSwitchTo == _audioSource.clip) {
                return;
            }
            _audioSource.clip = soundToSwitchTo;
            _audioSource.Play();
        }
        #endregion

        #region Persistence
        public OptionValues LoadOptions() {
            return _volume;
        }

        /// <summary>
        /// This will load ALL saved player data into this GameController 
        /// instance from wherver the users browsersaves data.
        /// </summary>
        protected internal void loadPreferences() {
            // Audio
            _volume = new OptionValues(
                (PlayerPrefs.HasKey("MasterVolume")) ? PlayerPrefs.GetFloat("MasterVolume") : 1F,
                (PlayerPrefs.HasKey("MusicVolume")) ? PlayerPrefs.GetFloat("MusicVolume") : 1F,
                (PlayerPrefs.HasKey("EffectVolume")) ? PlayerPrefs.GetFloat("EffectVolume") : 1F);
            _audioSource.volume = this._volume.Master;
            AudioListener.volume = this._volume.Music;

            // Tokens
            _tokens = (PlayerPrefs.HasKey("Tokens")) ? PlayerPrefs.GetInt("Tokens") : 0;
        }
        #endregion

        #region Helper methods
        public void AddToken() {
            this._tokens++;
        }
        #endregion
    }

    /// <summary>
    /// Struct that holds all the values that can be in volume options
    /// </summary>
    public struct OptionValues {
        private float master;
        public float Master
        {
            get
            {
                return master;
            }
            set
            {
                master = value;
                if (iterable == null) {
                    iterable = new float[3];
                    iterable[1] = 1F;
                    iterable[2] = 1F;
                }
                iterable[0] = value;
            }
        }
        private float music;
        public float Music
        {
            get
            {
                return music;
            }
            set
            {
                music = value;
                if (iterable == null) {
                    iterable = new float[3];
                    iterable[0] = 1F;
                    iterable[1] = 1F;
                }
                iterable[2] = value;
            }
        }
        private float effects;
        public float Effects
        {
            get
            {
                return effects;
            }
            set
            {
                effects = value;
                if (iterable == null) {
                    iterable = new float[3];
                    iterable[0] = 1F;
                    iterable[2] = 1F;
                }
                iterable[1] = value;
            }
        }

        public float[] iterable;

        public OptionValues(float master, float music, float effects) {
            this.master = master;
            this.music = music;
            this.effects = effects;

            float[] temp = { master, effects, music };
            this.iterable = temp;
        }
    }
}