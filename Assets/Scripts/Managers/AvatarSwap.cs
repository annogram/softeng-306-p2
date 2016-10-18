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
			this.NextSkin (player1);
		}
			
		public void PreviousPlayerOne(){
			this.PrevSkin (player1);
		}

		public void NextPlayerTwo() {
			this.NextSkin (player2);
		}

		public void PreviousPlayerTwo(){
			this.PrevSkin (player2);
		}

		public void SubmitSkinsToController(){
			// call game controller to notify of selection
		}

		private void NextSkin(SkinColour current){
			// switch case to determine next skin in list of skins
		}

		private void PrevSkin(SkinColour current){
			// switch case to determine prev skin in list of skins
		}
	}
}