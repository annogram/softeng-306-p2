using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

///<system>
/// This class is responsible for the pause logic
///</system>
public class PauseManager : MonoBehaviour {
	public GameObject pausePanel;
	public Camera cam;

	// Update is called once per frame
	void Update () {

		// If the P key is pushed bring up the pause menu
		if (Input.GetKeyDown (KeyCode.P)) {
			if (!pausePanel.active) {
				pausePanel.SetActive (true);
				cam.GetComponent<Blur>().enabled = true;
				Time.timeScale = 0.0f;
			} else {
				resume ();
			}
		}
	}

	// This method resumes the game from the pause menu
	public void resume () {
		pausePanel.SetActive (false);
		cam.GetComponent<Blur>().enabled = false;
		Time.timeScale = 1.0f;
	}


}
