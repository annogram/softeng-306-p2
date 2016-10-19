using UnityEngine;
using System.Collections;
using Managers;

public class HighscoreScreen : MonoBehaviour {

	public GameObject highscreenPopup;

	private GameController instance;

	void Start () {
		instance = GameController.Instance;
	}
	
	void HighscoreScreenRequested() {

		if (instance.LoggedIn) {
			instance.loadScreenSingle ("HSScreen");
		} else {
			// show popup
		}

	}
}
