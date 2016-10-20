using UnityEngine;
using System.Collections;
///<summary>
/// This singleton class is responsible for the bg music persistence
///</summary>
public class BackgroundAudioPersistence : MonoBehaviour {

	private static BackgroundAudioPersistence instance = null;

	public static BackgroundAudioPersistence Instance {
		get { return instance; }
	}

	void Awake(){
		if (instance != null && instance != this) {
			Destroy (this.gameObject);
			return;
		}
		else {
			instance = this;
		}
		DontDestroyOnLoad (transform.gameObject);
	}

	public void DestroyManually(){
		Destroy (this.gameObject);
	}
}
