using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

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
