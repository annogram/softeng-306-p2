using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

///<system>
/// This class is responsible for the view map logic which brings up the map from
/// in-game
///</system>

public class ViewMap : MonoBehaviour {

	public Camera mainCam;
	public GameObject mapCam;

	// This method brings up the map view in-game
	public void toggleMapView () {
		mainCam.GetComponent<Blur> ().enabled = !mainCam.GetComponent<Blur> ().enabled;
		//mainCam.enabled = false;
		mapCam.SetActive(!mapCam.active);
	}

}
