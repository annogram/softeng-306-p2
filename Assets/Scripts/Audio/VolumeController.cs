using UnityEngine;
using System.Collections;

public class VolumeController : MonoBehaviour {

	public void adjustMasterVolume(float volume){
		AudioListener.volume = volume;
	}
}
