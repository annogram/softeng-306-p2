using UnityEngine;
using System.Collections;

///<summary>
/// This class is responsible for the moving platform
///</summary>

public class MovingPlatformController : MonoBehaviour {

    public float baseValue = 0f;
    private Vector3 moveDown = new Vector3(0, -0.1F, 0);
    private Vector3 moveUp = new Vector3(0, 0.1F, 0);
    private Vector3 move = new Vector3(0, 0.1F, 0);

	// Update is called once per frame
  // This method moves the platform between a range
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
