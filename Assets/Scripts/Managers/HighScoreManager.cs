using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;

namespace Managers{
	
public class HighScoreManager : MonoBehaviour {

		//private GameController _gameController;
		public GameObject ScrollContain;
		// Use this for initialization
		void Start () {
			
			int level = 0;
			int totalScore = 0;
			foreach (Transform entry in ScrollContain.transform){
				//GameController.Instance.AddToken (3);
				//GameController.Instance.AddToken (3);
				//GameController.Instance.AddToken (5);
				int levelScore = GameController.Instance.GetTokensCollectedOnLevel (level);
				Transform currentHS = entry.transform.Find ("ScorePoints");
				Transform label = entry.transform.Find ("ScoreText");
				Text ScorePoints = currentHS.GetComponent<Text> ();
				Text ScoreText = label.GetComponent<Text> ();
				if (ScoreText.text == "Total" || ScoreText.text == "Endless Mode") {
					continue;
				}

				ScorePoints.text = levelScore.ToString();
				totalScore += levelScore;
				//Debug.Log (ScorePoints.text + score.ToString () + " on lvl " + level.ToString());
				level++;
			}
			Transform totalEntry = ScrollContain.transform.GetChild (0);
			Transform total = totalEntry.transform.Find ("ScorePoints");
			Text totalText = total.GetComponent<Text> ();
			totalText.text = totalScore.ToString ();

			Transform endlessEntry = ScrollContain.transform.GetChild (1);
			Transform endless = endlessEntry.transform.Find ("ScorePoints");
			Text endlessText = endless.GetComponent<Text> ();

			// Waiting on method to be written in GameController
			//int endlessScore = GameController.Instance.GetEndlessScore ();

			//Using placeholder for now
			int endlessScore = 5;
			endlessText.text = endlessScore.ToString ();
		
		}
		
		// Update is called once per frame
		void Update () {
		
		}
	}
}
