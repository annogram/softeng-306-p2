using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class LevelCompleteManager : MonoBehaviour {
	public GameObject completedPanel;
	public Camera cam;
	// Use this for initialization
	void Start () {
		//Time.timeScale = 1;
	}

	// Update is called once per frame
	void Update () {


		if (Input.GetKeyDown (KeyCode.L)) {
			if (!completedPanel.active) {
				completedPanel.SetActive (true);
				cam.GetComponent<Blur>().enabled = true;
				Time.timeScale = 0.0f;
			} else {
				resume ();
			}
		}
	}

	public void resume () {
		completedPanel.SetActive (false);
		cam.GetComponent<Blur>().enabled = false;
		Time.timeScale = 1.0f;
	}


}
