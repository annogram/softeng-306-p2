using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Managers;
using UnityStandardAssets.ImageEffects;
using UnityEngine.UI;

public class BoundScript : MonoBehaviour
{
    private GameController _controller;
    public string nextScene;
    public AudioClip ExitClip;
    public GameObject completedPanel;
    public Camera cam;
    public int currentLevel;
    private AudioSource _exitAudio;
    private LeaderboardController lb;    

    void Start() {
        _controller = GameController.Instance;
        _exitAudio = GetComponent<AudioSource>();
        _exitAudio.clip = ExitClip;
        lb = LeaderboardController.Instance;
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Player2") {
            _exitAudio.volume = _controller.GetSFXVolume();
            _exitAudio.Play();
            completedPanel.SetActive(true);
            //lb.startPostScores();
            cam.GetComponent<Blur>().enabled = true;
            Time.timeScale = 0.0f;
            //updateScores();
            // GameController controller = GameController.Instance;
            //controller.loadScreenSingle(nextScene);
        }
    }

    private void updateScores() {

        int levelScore = GameController.Instance.GetTokensCollectedOnCurrentLevel();
        int totalScore = GameController.Instance.GetTotalTokens();

        Transform scores = completedPanel.transform.GetChild(1);
        Transform levelEntry = scores.transform.Find("LevelScorePoints");
        Transform totalEntry = scores.transform.Find("TotalScorePoints");

        Text LevelScorePoints = levelEntry.GetComponent<Text>();
        Text TotalScorePoints = totalEntry.GetComponent<Text>();

        LevelScorePoints.text = levelScore.ToString();
        TotalScorePoints.text = totalScore.ToString();
    }

}