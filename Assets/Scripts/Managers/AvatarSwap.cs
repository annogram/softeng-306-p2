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

        public Sprite BlueIdle;
        public Sprite GreenIdle;
        public Sprite RedIdle;
        public Sprite PurpleIdle;

        private GameController controller;
		private SkinColour _player1Colour;
		private SkinColour _player2Colour;
        private GameObject _player1Sphere;
        private GameObject _player2Sphere;

		void Start() {
			this.controller = GameController.Instance;
            _player1Sphere = GameObject.Find("Player1Sphere");
            _player2Sphere = GameObject.Find("Player2Sphere");
            _player1Colour = SkinColour.BLUE;
            _player2Colour = SkinColour.RED;
        }
			
		public void TogglePlayerOne() {
            if (_player1Colour == SkinColour.BLUE)
            {
                _player1Sphere.GetComponent<SpriteRenderer>().sprite = GreenIdle;
                _player1Colour = SkinColour.GREEN;
            }
            else
            {
                _player1Sphere.GetComponent<SpriteRenderer>().sprite = BlueIdle;
                _player1Colour = SkinColour.BLUE;
            }
            
		}

		public void TogglePlayerTwo() {
            if (_player2Colour == SkinColour.RED)
            {
                _player2Sphere.GetComponent<SpriteRenderer>().sprite = PurpleIdle;
                _player2Colour = SkinColour.PURPLE;
            }
            else
            {
                _player2Sphere.GetComponent<SpriteRenderer>().sprite = RedIdle;
                _player2Colour = SkinColour.RED;
            }
        }
			

		public void SubmitSkinsToController(){
			controller.SaveAvatarSelection (_player1Colour, _player2Colour);
		}
			
	}
}