using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEditor.SceneManagement;
using Managers;

/// <summary>
/// Level exit will determine the behavior of swiching levels
/// </summary>
public class LevelExit : MonoBehaviour {
    public string nextScene;
    public AudioClip ExitClip;

    private bool Playa1IsInDaHouse = false;
    private bool Playa2IsInDaHouse = false;
    private GameController _gameController;
    private AudioSource _exitAudio;

    // Use this for initialization
    void Start()
    {
        _gameController = GameController.Instance;
        _exitAudio = GetComponent<AudioSource>();
        _exitAudio.clip = ExitClip;
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
            GameController controller = GameController.Instance;
            controller.loadScreenSingle(nextScene);
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Player") {
            Playa1IsInDaHouse = false;
        } else if (other.tag == "Player2") {
            Playa2IsInDaHouse = false;
        }
    }
}
