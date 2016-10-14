using UnityEngine;
using System.Collections;

public class SpringController : MonoBehaviour {

    public GameObject spring;
    public GameObject head;

    public Vector3 cfVector = new Vector3(0.05F, 0, 0);
    public bool compressLeft = true;
    public float minWidth = 10.0F;
    public float maxWidth = 30.0F;
    public Vector2 exitForce = new Vector2(5000F, 0);

    private PlayerController pc;
    private float bigCompressFactor = 0.5F;
    private float compressOrientation = 1.0F;
    private bool shouldCompress = false;

	// Use this for initialization
	void Start () {
        compressOrientation = compressLeft ? -1.0F : 1.0F;
    }

    void OnTriggerEnter2D(Collider2D col) {

        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Player2") {
            pc = col.gameObject.GetComponent<PlayerController>();
            shouldCompress = true;
        }

    }

    void OnTriggerExit2D(Collider2D col) {

        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Player2") {
            shouldCompress = false;
            Vector2 currentExitForce = new Vector2((1 - (spring.transform.localScale.x - minWidth) / (maxWidth - minWidth)) * (exitForce.x), 0);
            if (pc.IsTouchingPlayer()) {
                currentExitForce = currentExitForce * 2;
            }
            col.gameObject.GetComponent<Rigidbody2D>().AddForce(currentExitForce);
        }
    }

    // Update is called once per frame
    void FixedUpdate () {

        if (shouldCompress) {
            CompressSpring();
        } else {
            ExpandSpring();
        }

    }

    private void ExpandSpring() {

        if (spring.transform.localScale.x < maxWidth)
        {
            spring.transform.localScale += cfVector * 10;

            spring.transform.position -= (cfVector * 10 * -compressOrientation / 2);
            head.transform.position -= (cfVector * 10 * - compressOrientation);
            this.transform.position -= (cfVector * 10 * - compressOrientation);
        }

    }

    private void CompressSpring() {

        bigCompressFactor = pc.IsTouchingPlayer() ? 1F : 0.3F;


        if (spring.transform.localScale.x > maxWidth - (maxWidth - minWidth) * bigCompressFactor) {
            spring.transform.localScale -= cfVector;

            spring.transform.position -= (cfVector * compressOrientation / 2);
            head.transform.position -= (cfVector * compressOrientation);
            this.transform.position -= (cfVector * compressOrientation);

        } else if (spring.transform.localScale.x < maxWidth - (maxWidth - minWidth) * bigCompressFactor) {
            ExpandSpring();
        }

    }
}
