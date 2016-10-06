using UnityEngine;
using System.Collections;

public class ChangeLevelPage : MonoBehaviour {

	public GameObject pageOne = null;
	public GameObject pageTwo = null;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void nextPage(){

		pageOne.SetActive (!pageOne.active);
		pageTwo.SetActive (!pageTwo.active);

	
	}
}
