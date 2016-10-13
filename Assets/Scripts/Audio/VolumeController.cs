using UnityEngine;
using System.Collections;

[System.Obsolete("GameController now manages changing volume in the option")]
public class VolumeController : MonoBehaviour {

    [System.Obsolete("Use GameController.adjustMasterVolume() instead")]
	public void adjustMasterVolume(float volume){
		AudioListener.volume = volume;
	}
}
