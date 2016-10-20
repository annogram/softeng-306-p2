using UnityEngine;
using System.Collections;

///<system>
/// This class is responsbile for quitting the game
///</system>
public class QuitGame : MonoBehaviour {

	// This method quits the application
	public void quitGame(){
		Application.Quit ();
	}
}
