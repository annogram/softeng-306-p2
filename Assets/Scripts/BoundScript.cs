using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Managers;

public class BoundScript : MonoBehaviour
{
    private GameController _controller;

    void Start() {
        _controller = GameController.Instance;
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Player2") {
            // Level complete here
            _controller.loadScreenSingle(SceneManager.GetActiveScene().name);
        }
    }
}