using UnityEngine;
using System.Collections;

public class SpringController : MonoBehaviour {

    public GameObject spring;
    public GameObject head;

    public float compressFactor = 0.05F;
    public Vector3 cfVector = new Vector3(0.05F, 0, 0);
    public bool compressLeft = true;
    public float minWidth = 10.0F;
    public float maxWidth = 30.0F;
    public Vector2 exitForce = new Vector2(2000F, 0);
    public LayerMask[] playerLayers;

    private PlayerController pc;
    private float bigCompressFactor = 0.5F;
    private bool bothPlayersPushing = false;
    private Rigidbody2D _springRb;
    private Rigidbody2D _headRb;
    private float compressOrientation = 1.0F;
    private bool shouldCompress = false;
    private Rigidbody2D _colRb;

	// Use this for initialization
	void Start () {
        compressOrientation = compressLeft ? -1.0F : 1.0F;
        _springRb = spring.GetComponent<Rigidbody2D>();
        _headRb = head.GetComponent<Rigidbody2D>();
        _colRb = this.GetComponent<Rigidbody2D>();

    }

    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Player2") Debug.Log("collided by " + col.gameObject.tag);
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Player2")
        {
            pc = col.gameObject.GetComponent<PlayerController>();
            Debug.Log("SETTING THE BOOL");
            shouldCompress = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Player2") Debug.Log("exited by " + col.gameObject.tag);
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Player2")
        {
            Debug.Log("SETTING THE BOOL");
            shouldCompress = false;
            Vector2 currentExitForce = new Vector2((1 - (spring.transform.localScale.x - minWidth) / (maxWidth - minWidth)) * (exitForce.x), 0);
            if (pc.IsTouchingPlayer())
            {
                currentExitForce = currentExitForce * 2;
            }
            col.gameObject.GetComponent<Rigidbody2D>().AddForce(currentExitForce);
        }
    }

    // Update is called once per frame
    void FixedUpdate () {
        if (shouldCompress) { CompressSpring(); }
        else { ExpandSpring(); }
    }

    private void ExpandSpring()
    {
        if (spring.transform.localScale.x < maxWidth)
        {
            Debug.Log("Expanding");
            spring.transform.localScale += cfVector * 10;
            //spring.transform.localScale += new Vector3(compressFactor * 10, 0, 0);
            //_springRb.MovePosition(spring.transform.position - (new Vector3((compressFactor * 10 * -compressOrientation) / 2, 0, 0)));
            //_headRb.MovePosition(head.transform.position - (new Vector3((compressFactor * 10 * -compressOrientation), 0, 0)));
            //_colRb.MovePosition(this.transform.position - (new Vector3((compressFactor * 10 * -compressOrientation), 0, 0)));

            spring.transform.position -= (cfVector * 10 * -compressOrientation / 2);
            head.transform.position -= (cfVector * 10 * - compressOrientation);
            this.transform.position -= (cfVector * 10 * - compressOrientation);
        }
    }

    private void CompressSpring()
    {
        bigCompressFactor = pc.IsTouchingPlayer() ? 1F : 0.3F;

        if (spring.transform.localScale.x > maxWidth - (maxWidth - minWidth) * bigCompressFactor)
        {
            Debug.Log("Compressing");
            spring.transform.localScale -= cfVector;
            //spring.transform.localScale -= new Vector3(compressFactor, 0, 0);
            //_springRb.MovePosition(spring.transform.position - (new Vector3((compressFactor * compressOrientation) / 2, 0, 0)));
            //_headRb.MovePosition(head.transform.position - (new Vector3((compressFactor * compressOrientation), 0, 0)));
            //_colRb.MovePosition(this.transform.position - (new Vector3((compressFactor * compressOrientation), 0, 0)));

            spring.transform.position -= (cfVector * compressOrientation / 2);
            head.transform.position -= (cfVector * compressOrientation);
            this.transform.position -= (cfVector * compressOrientation);

        } else if(spring.transform.localScale.x < maxWidth - (maxWidth - minWidth) * bigCompressFactor)
        {
            ExpandSpring();
        } 

    }
}
