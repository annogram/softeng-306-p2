using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class ViewMap : MonoBehaviour {

	public Camera mainCam;
	public GameObject mapCam;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	
	}

	public void toggleMapView () {
		mainCam.GetComponent<Blur> ().enabled = !mainCam.GetComponent<Blur> ().enabled;
		//mainCam.enabled = false;
		mapCam.SetActive(!mapCam.active);
	} 
		
}
