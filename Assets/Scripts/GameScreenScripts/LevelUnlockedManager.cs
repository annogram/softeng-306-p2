using UnityEngine;
using System.Collections;
using Managers;
using UnityEngine.UI;

///<summary>
/// This class is responsible for unlocking level logic
///</summary>
public class LevelUnlockedManager : MonoBehaviour {

    private GameController _controller;
    public Button[] levelButtons;

	// This method is for initialization
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
