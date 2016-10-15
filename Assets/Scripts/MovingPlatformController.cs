using UnityEngine;
using System.Collections;

public class MovingPlatformController : MonoBehaviour {

    private int baseValue = 0;
    private Vector3 moveDown = new Vector3(0, -0.3F, 0);
    private Vector3 moveUp = new Vector3(0, 0.3F, 0);
    private Vector3 move = new Vector3(0, 0.3F, 0);

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position += move;
	    if(this.transform.position.y > 10)
        {
            move = moveDown;
        } 
        if(this.transform.position.y < -10)
        {
            move = moveUp;
        }
	}
}
