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
				//Test values
				//GameController.Instance.AddToken (3);
				//GameController.Instance.AddToken (3);
				//GameController.Instance.AddToken (5);

				//Go through the list of levels, update scores accordfngly
				int levelScore = GameController.Instance.GetTokensCollectedOnLevel (level);
				Transform currentHS = entry.transform.Find ("ScorePoints");
				Transform label = entry.transform.Find ("ScoreText");
				Text ScorePoints = currentHS.GetComponent<Text> ();
				Text ScoreText = label.GetComponent<Text> ();
				if (ScoreText.text == "Total Level Score" || ScoreText.text == "Endless Mode") {
					continue;
				}

				ScorePoints.text = levelScore.ToString();
				totalScore += levelScore;
				level++;
			}

			//Update total score count
			Transform totalEntry = ScrollContain.transform.GetChild (1);
			Transform total = totalEntry.transform.Find ("ScorePoints");
			Text totalText = total.GetComponent<Text> ();
			totalText.text = totalScore.ToString ();

			//Update endless score count
			Transform endlessEntry = ScrollContain.transform.GetChild (0);
			Transform endless = endlessEntry.transform.Find ("ScorePoints");
			Text endlessText = endless.GetComponent<Text> ();
			int endlessScore = GameController.Instance.GetEndlessHighscore ();
			endlessText.text = endlessScore.ToString ();
		
		}
		
		// Update is called once per frame
		void Update () {
		
		}
	}
}
