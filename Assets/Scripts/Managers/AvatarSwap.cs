using UnityEngine;
using System.Collections;

namespace Managers {
	public class AvatarSwap : MonoBehaviour {

		private GameController controller;


		void Start() {
			this.controller = GameController.Instance;
		}
			
		public void NextPlayerOne() {
			// toggle to next avatar
		}
			
		public void PreviousPlayerOne(){
			// go back to previous avatar
		}

		public void NextPlayerTwo() {


		}

		public void PreviousPlayerTwo(){

		}
	}
}