﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Scrolllist : MonoBehaviour {
	// Use this for initialization

	private static Scrolllist instance5;
	
	public static Scrolllist Instance
	{
		get { return instance5; }
	}
	void Awake() {
		
		//DontDestroyOnLoad (gameObject);
		// If no Player ever existed, we are it.
		if (instance5 == null)
			instance5 = this;
		// If one already exist, it's because it came from another level.
		else if (instance5 != this) {
			Destroy (gameObject);
			return;
		}
	}
//____________________________________________________________

	public GameObject ScrollEntry;
	public GameObject ScrollContain;
	public int yourPosition;
	public GameObject LoadingText;
	public bool loading = true;
	public bool error = false;

	void Update () {
	
		if (!loading)
			LoadingText.SetActive (false);
		else
			LoadingText.SetActive (true);

		if (error)
			LoadingText.GetComponentInChildren<Text> ().text = "Could not contact server, please check connection";
	}

	public void getScrollEntrys()
	{
		//Destroy Objects that exists, because of a possible Call before
		foreach (Transform childTransform in ScrollContain.transform) Destroy(childTransform.gameObject);

		int j = 1;
		for (int i=0; i<LeaderboardController.Instance.onlineHighscore.Length-1; i++) {
			GameObject ScorePanel;
			ScorePanel = Instantiate (ScrollEntry) as GameObject;
			ScorePanel.transform.parent = ScrollContain.transform;
			ScorePanel.transform.localScale = ScrollContain.transform.localScale;
			Transform ThisScoreName = ScorePanel.transform.Find ("ScoreText");
			Text ScoreName = ThisScoreName.GetComponent<Text> ();
			//
			Transform ThisScorePoints = ScorePanel.transform.Find ("ScorePoints");
			Text ScorePoints = ThisScorePoints.GetComponent<Text> ();
			//
			Transform ThisScorePosition = ScorePanel.transform.Find ("ScorePosition");
			Text ScorePosition = ThisScorePosition.GetComponent<Text> ();

			//first position is green
			if (j==1)
			{
				ScorePanel.GetComponent<Image> ().color = new Color32( 0xFF, 0xFF, 0xFF, 0xE0 );
				Color32 c = new Color32(0x4C,0x47,0x47,0xFF);
				ScoreName.color = c;
				ScorePoints.color = c;
				ScorePosition.color = c;
			}
			ScorePosition.text = j+". ";
			string helpString = "";

			helpString = helpString+LeaderboardController.Instance.onlineHighscore [i]+" ";
			i++;

			ScoreName.text = helpString;

			//
			ScorePoints.text = LeaderboardController.Instance.onlineHighscore [i];

			if(LeaderboardController.Instance.onlineHighscore [i]=="9999")
			{
				ScoreName.color=Color.red;
				ScorePoints.color=Color.red;
				ScorePosition.color=Color.red;
				yourPosition = j;
			}
			j++;

		}

	}
}
