using UnityEngine;
using System.Collections;

public class PauseManager : MonoBehaviour {
	public GameObject pausePanel;
	// Use this for initialization
	void Start () {
		//Time.timeScale = 1;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.P)) {
			pausePanel.SetActive (!pausePanel.active);
			Time.timeScale = 0.0f;
		}
	
	}

	public void resume () {
		pausePanel.SetActive (false);
		Time.timeScale = 1.0f;
	}
		
		
}
