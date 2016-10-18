using UnityEngine;
using System.Collections;
using Managers;
using UnityEngine.UI;

public class LevelUnlockedManager : MonoBehaviour {

    private GameController _controller;
    public Button[] levelButtons;

	// Use this for initialization
	void Start () {
        _controller = GameController.Instance;

        for (int i = 0; i < _controller.LevelsUnlocked; i++)
        {
            Debug.Log(i);
            levelButtons[i].interactable = true;
            Debug.Log(i);
        }
	}
	
}
