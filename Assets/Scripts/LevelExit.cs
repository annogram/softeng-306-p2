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

    void OnTriggerEnter2D(Collider2D other) {
        GameController controller = GameController.Instance;
        controller.loadScreenSingle(nextScene);
    }
}
