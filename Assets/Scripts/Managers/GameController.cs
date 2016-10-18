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

		private const int TOTAL_NUMBER_OF_LEVELS = 7; 

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

        private int _levelsUnlocked;
        public int LevelsUnlocked
        {
            get
            {
                return _levelsUnlocked;
            }
            set
            {
                _levelsUnlocked = (value >= TOTAL_NUMBER_OF_LEVELS) ? TOTAL_NUMBER_OF_LEVELS : value;
                Debug.Log(_levelsUnlocked);
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
		private int _currentLevelTokens = 0;
		private int[] _tokensCollectedAcrossGame = new int[TOTAL_NUMBER_OF_LEVELS];

        #endregion

        #region Constructor
        void Awake() {
            PlayerPrefs.DeleteAll();
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

        #region Screen management
        public void loadScreenSingle(string screenName) {
            //Debug.Log(string.Format("changing screens to {0}", screenName));
            if (SceneManager.GetActiveScene().name != screenName) {
                ChangeAudio(screenName);
            }
            
			ResetTokenCollectionOnCurrentLevel ();
            SceneManager.LoadScene(screenName, LoadSceneMode.Single);
        }

        public void loadScreenAdditive(string screenName) {
            ChangeAudio(screenName);
			ResetTokenCollectionOnCurrentLevel ();
            SceneManager.LoadScene(screenName, LoadSceneMode.Additive);
        }

        public void restartCurrentScene() {
            int scene = SceneManager.GetActiveScene().buildIndex;
			ResetTokenCollectionOnCurrentLevel ();
            SceneManager.LoadScene(scene, LoadSceneMode.Single);
        }
        #endregion

        #region Audio management
        internal void AdjustMasterVolume(float volume) {
            _volume.Master = volume;
            AudioListener.volume = _volume.Master;
            //PlayerPrefs.SetFloat("MasterVolume", _volume.Master);
            //PlayerPrefs.Save();
        }

        internal void AdjustMusicVolume(float volume) {
            _volume.Music = volume;
            _audioSource.volume = _volume.Music;
            //PlayerPrefs.SetFloat("MusicVolume", _volume.Music);
            //PlayerPrefs.Save();
        }

        internal void AdjustEffectVolume(float volume) {
            _volume.Effects = volume;
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
        /// <summary>
        /// Used in <code>OptionsBehavior</code> to load in persistent values
        /// </summary>
        /// <returns>Structure holding volume values</returns>
        public OptionValues LoadOptions() {
            return _volume;
        }

        /// <summary>
        /// This will load ALL saved player data into this GameController 
        /// instance from wherver the users browsersaves data.
        /// </summary>
        protected internal void loadPreferences() {
			// Audio
			_volume = new OptionValues (
				(PlayerPrefs.HasKey ("MasterVolume")) ? PlayerPrefs.GetFloat ("MasterVolume") : 1F,
				(PlayerPrefs.HasKey ("MusicVolume")) ? PlayerPrefs.GetFloat ("MusicVolume") : 1F,
				(PlayerPrefs.HasKey ("EffectVolume")) ? PlayerPrefs.GetFloat ("EffectVolume") : 1F);
			_audioSource.volume = this._volume.Master;
			AudioListener.volume = this._volume.Music;

			// Tokens
			_tokens = (PlayerPrefs.HasKey ("Tokens")) ? PlayerPrefs.GetInt ("Tokens") : 0;
			_currentLevelTokens = 0;

			if (PlayerPrefs.HasKey ("TokensCollectedAcrossGame")) {
				Debug.Log ("Tokens collected across game being loaded from persistence.");
				string persistedTokenString = PlayerPrefs.GetString ("TokensCollectedAcrossGame");
				Debug.Log ("Persistence token string found :" + persistedTokenString);
				this.ConvertStringToTokensCollected (persistedTokenString);
			} else {
				Debug.Log ("No persistence found for tokens collected. Re-initalising instead!");
				this.LoadInitialTokenPersistenceArray();
			}

            // Levels
            _levelsUnlocked = (PlayerPrefs.HasKey("LevelsUnlocked")) ? PlayerPrefs.GetInt("LevelsUnlocked") : 1;

        }

        void OnDestroy() {
            PlayerPrefs.SetFloat("MasterVolume", _volume.Master);
            PlayerPrefs.SetFloat("MusicVolume", _volume.Music);
            PlayerPrefs.SetFloat("EffectVolume", _volume.Effects);
            PlayerPrefs.SetInt("Tokens", _tokens);
			PlayerPrefs.SetString ("TokensCollectedAcrossGame", ConvertTokensCollectedToString());
            PlayerPrefs.SetInt("LevelsUnlocked", _levelsUnlocked);
            PlayerPrefs.Save();
        }
        #endregion

        #region Externally called handler methods
        [System.Obsolete("Use AddToken(int Level)")]
        public void AddToken() {
			this._tokens++;
			this._currentLevelTokens++;
        }

		public void AddToken(int level){
			Debug.Log (string.Format ("Token collected by player on level {0}", level));
			this._tokens++;
			this._currentLevelTokens++;
			UpdateTokenPersistenceArray (this._currentLevelTokens, level);
		}

		public int GetTokensCollectedOnLevel(int level){
			if (level >= _tokensCollectedAcrossGame.Length || level < 0) {
				return -1;
			}
			return _tokensCollectedAcrossGame [level];
		}

		public int GetTokensCollectedOnCurrentLevel(){
			return _currentLevelTokens;
		}

		public int GetTotalTokens() {
			int total = 0;
			foreach (int score in _tokensCollectedAcrossGame) {
				total += score;
			}
			return total;
		}

        internal float GetSFXVolume() {
            return _volume.Effects;
        }
        #endregion

		#region Token Management
		private void UpdateTokenPersistenceArray(int tokensCollectedValue, int level){
			if (level > _tokensCollectedAcrossGame.Length || level < 0) {
				Debug.LogError ("Level " + level + " out of bounds! Must be between 0 and " + _tokensCollectedAcrossGame.Length);
				return;
			}

			level = level - 1;
				
			if (_tokensCollectedAcrossGame [level] < tokensCollectedValue) {
				Debug.Log (string.Format("Updating tokens collected for Level {0} to {1}", level + 1, tokensCollectedValue));
				_tokensCollectedAcrossGame[level] = tokensCollectedValue;
			}
		}

		private void ResetTokenCollectionOnCurrentLevel(){
			Debug.Log ("Resetting tokens collected for current level to 0");
			_currentLevelTokens = 0;
		}

		private void ConvertStringToTokensCollected(string marshalledArray){
			Debug.Log ("Converting " + marshalledArray + " to tokens array.");
			_tokensCollectedAcrossGame = new int[TOTAL_NUMBER_OF_LEVELS];

			for (int i = 0; i < TOTAL_NUMBER_OF_LEVELS; ++i) {
				this._tokensCollectedAcrossGame [i] = int.Parse("" + marshalledArray.ElementAt(i));
			}

			Debug.Log (_tokensCollectedAcrossGame[0]);
		}

		private string ConvertTokensCollectedToString(){
			char[] tokenCharArray = _tokensCollectedAcrossGame.Select (s => ("" + s).ToCharArray().First()).ToArray ();
			string tokensString = new string (tokenCharArray);
			Debug.Log ("Converted tokens array to string: " + tokensString);
			return tokensString;
		}

		private void LoadInitialTokenPersistenceArray(){
			Debug.Log ("Re-initialising token array to " + TOTAL_NUMBER_OF_LEVELS + " zeros.");
			for (int i = 0; i < TOTAL_NUMBER_OF_LEVELS; ++i) {
				this._tokensCollectedAcrossGame [i] = 0;
			}
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