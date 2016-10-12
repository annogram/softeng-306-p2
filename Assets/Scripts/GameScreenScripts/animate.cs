using UnityEngine;
using System.Collections;

public class animate : MonoBehaviour {
	protected Animator obsanim;

	// Use this for initialization
	void Start () {
		obsanim = GetComponent<Animator> ();
		obsanim.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey (KeyCode.UpArrow)) {
			obsanim.enabled = true;
			obsanim.Play ("StartSlideOut");
			obsanim.Play ("AboutPageSlide");
		}

		if (Input.GetKey (KeyCode.LeftArrow)) {
			obsanim.Play ("StartSlideIn");
			obsanim.Play ("AboutPageSlideOut");
		}
	
	}
}
