using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BgScript : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Player2") {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
