using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
    private static GameController _instance;

    void Awake() {
        if (_instance == null) {
            _instance = this;
        } 

    }

    // Update is called once per frame
    void Update() {
    }

    #region Helper methods
    #endregion
}
