using UnityEngine;
using UnityEngine.UI;
using System.Collections;


namespace Managers {
	
	public class LoginManager : MonoBehaviour {

		public InputField inputField;

		private const string NEXT_SCENE_FOLLOWING_LOGIN = "AvatarSelectScreen";

		private GameController instance;

		private Text messageText;
	
		void Start () {
			instance = GameController.Instance;
			Transform message = this.gameObject.transform.Find ("Message");
			messageText = message.GetComponent<Text> ();
		}

		public void NewGame() {
			bool attempt = instance.attemptTeamNewGame (inputField.text);
			if (attempt) {
				instance.loadScreenSingle (NEXT_SCENE_FOLLOWING_LOGIN);
				messageText.text = "";
			} else {
				Transform message = this.gameObject.transform.Find ("Message");
				messageText.text = "Team already exists! Can't create new game.";
			}
		}

		void LoadGame() {
			bool attempt = instance.attemptTeamLoadGame (inputField.text);
			if (attempt) {
				instance.loadScreenSingle(NEXT_SCENE_FOLLOWING_LOGIN);
			}
		}

	}

}
