using UnityEngine;
using System.Collections;

///<summary>
/// This class is responsbile for the teleporter logic
///</summary>
public class TeleporterScript : MonoBehaviour {

	public GameObject teleportDestination;

	// This method is called when an object enters the box collider for the Teleporter
	void OnTriggerEnter2D(Collider2D other){
		Debug.Log ("Teleporter touched by " + other.tag);

		if (!(other.tag == "Player" || other.tag == "Player2")) {
			return;
		}
		//Teleport the object to the destination
		other.transform.position = teleportDestination.transform.position;
	}
}
