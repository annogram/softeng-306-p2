using UnityEngine;
using System.Collections;

public class AnimateTransition : MonoBehaviour {
	public Animator startAnim;
	public Animator aboutAnim;

	// Use this for initialization
	void Start () {
		startAnim.enabled = false;
		aboutAnim.enabled = false;
	}

	// Update is called once per frame
	void Update () {

	}

	public void showStartPage(){
		startAnim.Play ("StartSlideIn");
		aboutAnim.Play ("AboutPageSlideOut");
	}

	public void showAboutPage(){
		startAnim.enabled = true;
		aboutAnim.enabled = true;
		startAnim.Play ("StartSlideOut");
		aboutAnim.Play ("AboutPageSlide");
	}
}
