using UnityEngine;
using System.Collections;
using Managers;

public class HighscoreScreen : MonoBehaviour {

	public GameObject highscreenPopup;

	private GameController instance;

	void Start () {
		instance = GameController.Instance;
	}
	
	public void HighscoreScreenRequested() {

		if (instance.LoggedIn) {
			instance.loadScreenSingle ("HSScreen");
		} else {
			// show popup
			togglePopup ();
		}

	}

	public void togglePopup(){
		highscreenPopup.SetActive (!highscreenPopup.active);
	}
}
