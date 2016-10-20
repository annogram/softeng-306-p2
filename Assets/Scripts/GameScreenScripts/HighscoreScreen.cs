using UnityEngine;
using System.Collections;
using Managers;
///<summary>
/// This class is responsible for dealing with the Highscore Screen transitions
///</summary>
public class HighscoreScreen : MonoBehaviour {

	public GameObject highscreenPopup;
	private GameController instance;

	/// This method is for initialization
	void Start () {
		instance = GameController.Instance;
	}

	// This method brings up the highscore screen or popup screen depending on login status
	public void HighscoreScreenRequested() {

		if (instance.LoggedIn) {
			instance.loadScreenSingle ("HSScreen");
		} else {
			// show popup
			togglePopup ();
		}

	}

	// This method toggles the popup screen
	public void togglePopup(){
		highscreenPopup.SetActive (!highscreenPopup.active);
	}
}
