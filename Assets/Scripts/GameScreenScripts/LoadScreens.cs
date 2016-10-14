using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

[System.Obsolete("Gamemanager now changes scenes")]
public class LoadScreens : MonoBehaviour {

	public void loadScreenSingle(string screenName){
		SceneManager.LoadScene (screenName, LoadSceneMode.Single);
	}

	public void loadScreenAdditive(string screenName){
		SceneManager.LoadScene (screenName, LoadSceneMode.Additive);
	}

	public void restartCurrentScene()
	{
		int scene = SceneManager.GetActiveScene().buildIndex;
		SceneManager.LoadScene(scene, LoadSceneMode.Single);
	}


}
