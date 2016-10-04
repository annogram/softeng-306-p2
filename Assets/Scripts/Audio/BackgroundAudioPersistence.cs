using UnityEngine;
using System.Collections;

public class BackgroundAudioPersistence : MonoBehaviour {

	void Awake(){
		DontDestroyOnLoad (transform.gameObject);
	}
}
