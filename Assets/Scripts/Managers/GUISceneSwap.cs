using UnityEngine;
using System.Collections;

namespace Managers {
    /// <summary>
    /// This class should be used in a canvas to assign behavior to button presses. 
    /// All canvas' should have a script component with this class which will pass 
    /// information into Next or NextAdditive to bring on the next scene.
    /// </summary>
    public class GUISceneSwap : MonoBehaviour {

        private GameController controller;
        // Use this for initialization
        void Start() {
            this.controller = GameController.Instance;
        }
        
        /// <summary>
        /// Calls on the gamecontroller instnace currently active to change the scene
        /// </summary>
        /// <param name="nextScene">A string reffering to a scene currently in the build</param>
        public void Next(string nextScene) {
            controller.loadScreenSingle(nextScene);
        }

        /// <summary>
        /// Calls on the gamecontroller instnace currently active to overlay a scene
        /// </summary>
        /// <param name="nextScene">A string reffering to a scene currently in the build</param>
        public void NextAdditive(string nextScene) {
            controller.loadScreenAdditive(nextScene);
        }

        /// <summary>
        /// Calls on the gamecontroller instance to restart the scene
        /// </summary>
        public void Restart() {
            controller.restartCurrentScene();
        }
    }
}