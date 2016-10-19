using UnityEngine;
using System.Collections;

public enum SkinColour {

	BLUE,
	RED,
	GREEN,
	PURPLE

}

namespace Managers {
	public class AvatarSwap : MonoBehaviour {

		private GameController controller;
		private SkinColour player1;
		private SkinColour player2;

		void Start() {
			this.controller = GameController.Instance;
		}
			
		public void TogglePlayerOne() {
            // TODO change the player image in scene
			player1 = player1 == SkinColour.BLUE ? SkinColour.GREEN : SkinColour.BLUE;
		}

		public void TogglePlayerTwo() {
            // TODO change the player image in scene
            player2 = player2 == SkinColour.RED ? SkinColour.PURPLE : SkinColour.RED;
		}
			

		public void SubmitSkinsToController(){
			controller.SaveAvatarSelection (player1, player2);
		}
			
	}
}