using UnityEngine;
using System.Collections;

///<summary>
/// This class is responsible for dealing with chaning the level selection page
///</summary>
public class ChangeLevelPage : MonoBehaviour {

	public GameObject pageOne = null;
	public GameObject pageTwo = null;

	// Changes to next page
	public void nextPage(){
		pageOne.SetActive (!pageOne.active);
		pageTwo.SetActive (!pageTwo.active);
	}
}
