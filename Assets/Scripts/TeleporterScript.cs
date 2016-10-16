using UnityEngine;
using System.Collections;

public class TeleporterScript : MonoBehaviour {

	public GameObject teleportDestination;

	void OnTriggerEnter2D(Collider2D other){
		Debug.Log ("Teleporter touched.");
		if (other.tag != "Player" || other.tag != "Player2") {
			return;
		}

		other.transform.position = teleportDestination.transform.position;
	}
}
