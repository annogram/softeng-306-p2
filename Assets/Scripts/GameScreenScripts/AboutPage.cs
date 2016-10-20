using UnityEngine;
using System.Collections;
///<summary>
/// This class is responsible for toggling the active page between start-page
/// and about page.
///</summary>
public class AboutPage : MonoBehaviour {
	public GameObject start;
	public GameObject about;

	//This method toggles the start page and about page
	public void togglePage(){

		start.SetActive (!start.active);
		about.SetActive (!about.active);

	}

}
