using UnityEngine;
using System.Collections;
using Managers;
/// <summary>
/// This class is responsible for animating the start, login, about-page transitions.
/// </summary>
public class AnimateTransition : MonoBehaviour {
	public Animator startAnim;
	public Animator aboutAnim;
	public Animator loginAnim;

	// This method is for initialization
	void Start () {
		startAnim.enabled = false;
		aboutAnim.enabled = false;
		loginAnim.enabled = false;
	}

	// This method animates the start page sliding in and the current page to slide out
	public void showStartPage(){
		startAnim.Play ("StartSlideIn");
		if (aboutAnim.enabled) {
			aboutAnim.Play ("AboutPageSlideOut");
		}
		if (loginAnim.enabled) {
			loginAnim.Play ("LoginPageSlideOut");
		}
	}

	// This method animates the about page sliding in and the current page to slide out
	public void showAboutPage(){
		startAnim.enabled = true;
		aboutAnim.enabled = true;
		startAnim.Play ("StartSlideOut");
		aboutAnim.Play ("AboutPageSlide");
	}

	// This method animates the login page sliding in and the current page to slide out
	public void showLoginPage(){
			startAnim.enabled = true;
			loginAnim.enabled = true;
			startAnim.Play ("StartSlideOut");
			loginAnim.Play ("LoginPageSlide");

	}
}
