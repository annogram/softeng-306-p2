using UnityEngine;
using System.Collections;

public class SpringController : MonoBehaviour {

    public GameObject spring;
    public GameObject head;

    public float compressFactor = 0.05F;
    public bool compressLeft = true;
    public float minWidth = 10.0F;
    public float maxWidth = 30.0F;
    public Vector2 exitForce = new Vector2(2000F, 0);

    private Rigidbody2D _springRb; 
    private float compressOrientation = 1.0F;
    private bool shouldCompress = false;

	// Use this for initialization
	void Start () {
        compressOrientation = compressLeft ? -1.0F : 1.0F;
        _springRb = spring.GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Player" || col.gameObject.tag == "Player2") Debug.Log("collided by " + col.gameObject.tag);
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Player2")
        {
            Debug.Log("SETTING THE BOOL");
            shouldCompress = true;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Player2") Debug.Log("exited by " + col.gameObject.tag);
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Player2")
        {
            Debug.Log("SETTING THE BOOL");
            shouldCompress = false;
            Vector2 currentExitForce = new Vector2((1 - (spring.transform.localScale.x - minWidth) / (maxWidth - minWidth)) * (exitForce.x), 0);
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
            spring.transform.localScale += new Vector3(compressFactor * 10, 0, 0);
            _springRb.MovePosition(spring.transform.position - (new Vector3((compressFactor * 10 * -compressOrientation) / 2, 0, 0)));


        }
    }

    private void CompressSpring()
    {
        if (spring.transform.localScale.x > minWidth)
        {
            Debug.Log("Compressing");

            spring.transform.localScale -= new Vector3(compressFactor, 0, 0);
            _springRb.MovePosition(spring.transform.position - (new Vector3((compressFactor * compressOrientation) / 2, 0, 0)));

        }

    }
}
