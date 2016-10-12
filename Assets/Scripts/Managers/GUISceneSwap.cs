using UnityEngine;
using System.Collections;

namespace Managers {
    public class GUISceneSwap : MonoBehaviour {

        private GameController controller;
        // Use this for initialization
        void Start() {
            this.controller = GameController.Instance;
        }
        
        public void Next(string nextScene) {
            controller.loadScreenSingle(nextScene);
        }
    }
}