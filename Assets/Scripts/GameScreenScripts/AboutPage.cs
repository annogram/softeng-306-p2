using UnityEngine;
using System.Collections;

public class AboutPage : MonoBehaviour {
	public GameObject start;
	public GameObject about;
	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
	}

	public void togglePage(){
		start.SetActive (!start.active);
		about.SetActive (!about.active);

	}

}
