using UnityEngine;
using System.Linq;
using System.Collections;

public class BumperScript : MonoBehaviour {

    public float Power;
    private Rigidbody2D _rb;

	// Use this for initialization
	void Start () {
        _rb = GetComponent<Rigidbody2D>();
	}

    void FixedUpdate() {

    }
    
    void OnCollisionEnter2D(Collision2D other) {
        Debug.Log(other.gameObject.name);
        Rigidbody2D playerBody = other.gameObject.GetComponent<Rigidbody2D>();
        // We need to get the contact points -centre of bumper
        var pointOfContact = other.contacts.FirstOrDefault();
        Vector2 launchTragectory = pointOfContact.normal.normalized;
        playerBody.AddForce(launchTragectory * -Power);
    }
}
