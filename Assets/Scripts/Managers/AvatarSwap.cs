using UnityEngine;
using System.Collections;

namespace Managers {
	public class AvatarSwap : MonoBehaviour {

		private enum SkinColour { RED, WHITE, BLUE, GREEN, YELLOW };

		private GameController controller;
		private SkinColour player1;
		private SkinColour player2;


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

		public void SubmitSkinsToController(){
			// call game controller to notify of selection
		}

		private void NextSkin(SkinColour current){

		}

		private void PrevSkin(SkinColour current){


		}
	}
}