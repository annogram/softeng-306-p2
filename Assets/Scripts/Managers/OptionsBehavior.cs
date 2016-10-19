using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Managers {
    /// <summary>
    /// This class is made for the options window to persistently store the volume
    /// throughout the game using calls to the game manager.
    /// <see cref="Managers.OptionValues"/>
    /// </summary>
    public class OptionsBehavior : MonoBehaviour {
        public Slider[] volumeSliders = new Slider[3];

        private GameController _controller;

        void Start() {
            _controller = GameController.Instance;

            OptionValues currentValues = _controller.LoadOptions();
            Debug.Log(string.Format("loaded in values {0},{1} and {2}", currentValues.Master,currentValues.Music,currentValues.Effects));
            int sliderNum = 0;
            // Iterate through the values in OptionValues to get the values for the sliders.
            // If there have been no values set in the lifetime of this game for persistence then 
            // the default value of 1F will be used
            foreach (Slider s in volumeSliders) {
                s.normalizedValue = (currentValues.iterable[sliderNum] != float.NaN)
                    ? currentValues.iterable[sliderNum] : 1F;
                sliderNum++;
            }
        }

        #region Controller Calls
        public void UpdateMaster(float volume) {
            _controller.AdjustMasterVolume(volume);
        }

        public void UpdateMusic(float volume) {
            _controller.AdjustMusicVolume(volume);
        }

        public void UpdateEffect(float volume) {
            _controller.AdjustEffectVolume(volume);
        }
        #endregion
    }
}