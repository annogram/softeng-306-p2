using UnityEngine;
using System.Collections;

public class MovingPlatformController : MonoBehaviour {

    private float baseValue = 0f;
    private Vector3 moveDown = new Vector3(0, -0.1F, 0);
    private Vector3 moveUp = new Vector3(0, 0.1F, 0);
    private Vector3 move = new Vector3(0, 0.1F, 0);

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position += move;
        baseValue += move.y;
	    if(baseValue > 10)
        {
            move = moveDown;
        } 
        if(baseValue < -30)
        {
            move = moveUp;
        }
	}
}
