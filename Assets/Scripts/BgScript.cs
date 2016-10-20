using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Managers;

///<summary>
/// This class is responsible for resetting the level when a player falls out the map
///</summary>

public class BgScript : MonoBehaviour {
    private GameController _controller;

    void Start() {
        _controller = GameController.Instance;
    }

    // This method resets the level on collision where a player model goes out of bounds
    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Player2") {
            _controller.restartCurrentScene();
        }
    }
}
