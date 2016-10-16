using UnityEngine;
using System.Collections;

public class EndlessMovement : MonoBehaviour {

    public GameObject player1;
    public GameObject player2;
    public GameObject boundary;
    public float maxSpeed = 100.0F;
    public float acceleration = 100.0F;

    public GameObject[] bumpers;
    public GameObject[] platforms;
    public GameObject cameraObject;

    private Rigidbody2D rb1;
    private Rigidbody2D rb2;
    private Rigidbody2D rbb;
    private Camera cam;

	// Use this for initialization
	void Start () {
        rb1 = player1.GetComponent<Rigidbody2D>();
        rb2 = player2.GetComponent<Rigidbody2D>();
        rbb = boundary.GetComponent<Rigidbody2D>();
        cam = cameraObject.GetComponent<Camera>();
    }

    // Update is called once per frame
    void FixedUpdate () {
        // Push players forward
	    if (rb1.velocity.x < maxSpeed) {
            rb1.AddForce(new Vector3(acceleration, 0));
        }
        if (rb2.velocity.x < maxSpeed) {
            rb2.AddForce(new Vector3(acceleration, 0));
        }

        // Update platform positions
        foreach(GameObject platform in platforms) {
            Rigidbody2D platformRb = platform.GetComponent<Rigidbody2D>();
            if ((platformRb.position.x + (platformRb.transform.localScale.x/2)) < rbb.position.x) {
                platformRb.transform.Translate(platformRb.position + new Vector2(platformRb.transform.localScale.x, 0));
            }
        }

        // Update boundary position
        Vector2 nextPos = rbb.position + new Vector2(maxSpeed/2, 0);
        Vector2 minPos = new Vector2(cam.transform.position.x - (cam.orthographicSize * Screen.width / Screen.height), 0);
        Vector2 newPos = nextPos.x > minPos.x ? new Vector2(nextPos.x * Time.fixedDeltaTime, rbb.position.y) : new Vector2(minPos.x * Time.fixedDeltaTime, rbb.position.y);
        rbb.MovePosition(newPos);
        Debug.Log(nextPos);

        // Update bumper positions



        // Update token positions
    }
}
