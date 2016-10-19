using UnityEngine;
using System.Collections;
using Managers;
using UnityEngine.UI;
using System;

public class HUDManager : MonoBehaviour {
	private GameObject _infoHUD;
	public int level;

	void Start () {
		_infoHUD = GameObject.FindGameObjectWithTag ("InfoHUD");

		//Set level number on GUI
		Transform levelInfo = _infoHUD.transform.Find ("LevelNumber");
		Text levelText = levelInfo.GetComponent<Text> ();
		levelText.text = "Level "+level.ToString ();

		//Set team name on GUI
		Transform teamInfo = _infoHUD.transform.Find ("TeamName");
		Text teamText = teamInfo.GetComponent<Text> ();
		string name = GameController.Instance.getCurrentTeam ();
		teamText.text = "Team "+name.ToString ();
	}

}
