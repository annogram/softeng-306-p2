using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEditor.SceneManagement;
using UnityStandardAssets.ImageEffects;
using Managers;

/// <summary>
/// Level exit will determine the behavior of swiching levels
/// </summary>
public class LevelExit : MonoBehaviour {
   // public string nextScene;

    private bool Playa1IsInDaHouse = false;
    private bool Playa2IsInDaHouse = false;
	public GameObject completedPanel;
	public Camera cam;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            Playa1IsInDaHouse = true;
        }else if(other.tag == "Player2") {
            Playa2IsInDaHouse = true;
        }
        if (Playa1IsInDaHouse && Playa2IsInDaHouse) {
			completedPanel.SetActive (true);
			cam.GetComponent<Blur>().enabled = true;
			Time.timeScale = 0.0f;

           // GameController controller = GameController.Instance;
            //controller.loadScreenSingle(nextScene);
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Player") {
            Playa1IsInDaHouse = false;
        } else if (other.tag == "Player2") {
            Playa2IsInDaHouse = false;
        }
    }

	public void resume () {
		completedPanel.SetActive (false);
		cam.GetComponent<Blur>().enabled = false;
		Time.timeScale = 1.0f;
	}

	public void loadLevelSelect(){
		resume ();
		GameController controller = GameController.Instance;
		controller.loadScreenSingle("LevelSelectScreen");
	}
}
