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

    private bool Playa1IsInDaHouse = false;
    private bool Playa2IsInDaHouse = false;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            Playa1IsInDaHouse = true;
        }else if(other.tag == "Player2") {
            Playa2IsInDaHouse = true;
        }
        if (Playa1IsInDaHouse && Playa2IsInDaHouse) {
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
