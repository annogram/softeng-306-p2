using UnityEngine;
using System.Collections;

public class BumperScript : MonoBehaviour {

    private Rigidbody2D _rb;

	// Use this for initialization
	void Start () {
        _rb = GetComponent<Rigidbody2D>();
	}

    void  OnCollisionEnter(Collision other) {
        Debug.Log("COLLISION DETECTED");
        other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(100000, 100000));
    }
}
