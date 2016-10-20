using UnityEngine;
using UnityEngine.UI;
using System.Collections;


namespace Managers {
	
	public class LoginManager : MonoBehaviour {

		public InputField inputField;

		private const string NEXT_SCENE_FOLLOWING_LOGIN = "AvatarSelectScreen";

		private GameController instance;

		private Text messageText;

		public bool newGame;
	
		void Start () {


			instance = GameController.Instance;
			Transform message = this.gameObject.transform.Find ("Message");
			messageText = message.GetComponent<Text> ();
			clearMessage ();
		}

		//Set which functionality is required
		public void setState(bool state){
			newGame = state;
		}

		//Login for desired load or new game
		public void login() {

			if (newGame)
				NewGame ();
			else
				LoadGame ();
		}

		void NewGame() {
			bool attempt = instance.attemptTeamNewGame (inputField.text);
			if (attempt) {
				successfullLogin ();
			} else {
				messageText.text = "Team already exists! Can't create new game.";
			}
		}
			
		void LoadGame() {
			bool attempt = instance.attemptTeamLoadGame (inputField.text);
			if (attempt) {
				successfullLogin ();
			} else {
				messageText.text = "No saved data for team!";
			}
		}

		//Complete the actions for setting team name and next scene
		private void successfullLogin(){
			clearMessage ();
			GameController.Instance._teamName = inputField.text;
			GameController.Instance.LoggedIn = true;
			instance.loadScreenSingle (NEXT_SCENE_FOLLOWING_LOGIN);
			Debug.Log ("Successful login!");
		}

		public void clearMessage(){
			messageText.text = "";
		}
			
	}

}
