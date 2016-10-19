using UnityEngine;
using System.Collections;
using Managers;
public class AnimateTransition : MonoBehaviour {
	public Animator startAnim;
	public Animator aboutAnim;
	public Animator loginAnim;

	// Use this for initialization
	void Start () {
		startAnim.enabled = false;
		aboutAnim.enabled = false;
		loginAnim.enabled = false;
	}

	// Update is called once per frame
	void Update () {

	}

	public void showStartPage(){
		startAnim.Play ("StartSlideIn");
		if (aboutAnim.enabled) {
			aboutAnim.Play ("AboutPageSlideOut");
		}
		if (loginAnim.enabled) {
			loginAnim.Play ("LoginPageSlideOut");
		}
	}

	public void showAboutPage(){
		startAnim.enabled = true;
		aboutAnim.enabled = true;
		startAnim.Play ("StartSlideOut");
		aboutAnim.Play ("AboutPageSlide");
	}

	public void showLoginPage(){
			startAnim.enabled = true;
			loginAnim.enabled = true;
			startAnim.Play ("StartSlideOut");
			loginAnim.Play ("LoginPageSlide");

	}
}
