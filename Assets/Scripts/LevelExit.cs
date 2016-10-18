using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;
using UnityEditor.SceneManagement;
using UnityStandardAssets.ImageEffects;
using Managers;

/// <summary>
/// Level exit will determine the behavior of swiching levels
/// </summary>
public class LevelExit : MonoBehaviour {
    public string nextScene;
    public AudioClip ExitClip;
    private bool Playa1IsInDaHouse = false;
    private bool Playa2IsInDaHouse = false;
    public GameObject completedPanel;
    public Camera cam;
    public int currentLevel;
    private GameController _gameController;
    private AudioSource _exitAudio;

    // Use this for initialization
    void Start()
    {
        _gameController = GameController.Instance;
        _exitAudio = GetComponent<AudioSource>();
        _exitAudio.clip = ExitClip;
        _gameController.LevelsUnlocked = currentLevel+1;
    }


    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            Playa1IsInDaHouse = true;
        }else if(other.tag == "Player2") {
            Playa2IsInDaHouse = true;
        }
        if (Playa1IsInDaHouse && Playa2IsInDaHouse) {
            _exitAudio.volume = _gameController.GetSFXVolume();
            _exitAudio.Play();
			completedPanel.SetActive (true);
			cam.GetComponent<Blur>().enabled = true;
			Time.timeScale = 0.0f;
			updateScores ();
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

	public void loadNextScene(string nextScene){
		resume ();
		GameController controller = GameController.Instance;
		controller.loadScreenSingle(nextScene);
	}

	private void updateScores() {
		
		int levelScore = GameController.Instance.GetTokensCollectedOnLevel (currentLevel-1);
		int totalScore = GameController.Instance.GetTotalTokens ();

		Transform scores = completedPanel.transform.GetChild(1);
		Transform levelEntry = scores.transform.Find ("LevelScorePoints");
		Transform totalEntry = scores.transform.Find ("TotalScorePoints");

		Text LevelScorePoints = levelEntry.GetComponent<Text> ();
		Text TotalScorePoints = totalEntry.GetComponent<Text> ();

		LevelScorePoints.text = levelScore.ToString ();
		TotalScorePoints.text = totalScore.ToString ();


	}
}
