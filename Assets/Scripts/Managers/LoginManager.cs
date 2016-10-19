using UnityEngine;
using UnityEngine.UI;
using System.Collections;


namespace Managers {
	
    /// <summary>
    /// Manages players logging into profiles from persistence and posting to the highscore
    /// board.
    /// </summary>
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

        /// <summary>
        /// Set which functionality is required
        /// </summary>
        /// <param name="state">Weather you're coming from a new game or a load game button</param>
        public void setState(bool state){
			newGame = state;
		}

        /// <summary>
		/// Login for desired load or new game
        /// Called when you either click load game or new game
        /// </summary>
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

        /// <summary>
        /// Complete the actions for setting team name and next scene
        /// </summary>
        private void successfullLogin(){
			clearMessage ();
			GameController.Instance._teamName = inputField.text;
			GameController.Instance.LoggedIn = true;
			instance.loadScreenSingle (NEXT_SCENE_FOLLOWING_LOGIN);
			Debug.Log ("Successful login!");
		}

        /// <summary>
        /// Clears the text feild if the user logs in with an invalid name
        /// </summary>
		public void clearMessage(){
			messageText.text = "";
		}
			
	}

}
