using UnityEngine;
using UnityEngine.UI;
using System.Collections;


namespace Managers {
	
	public class LoginManager : MonoBehaviour {

		public InputField inputField;

		private const string NEXT_SCENE_FOLLOWING_LOGIN = "AvatarSelectScreen";

		private GameController instance;
	
		void Start () {
			instance = GameController.Instance;
		}

		void NewGame() {
			bool attempt = instance.attemptTeamNewGame (inputField.text);
			if (attempt) {
				instance.loadScreenSingle(NEXT_SCENE_FOLLOWING_LOGIN);
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
