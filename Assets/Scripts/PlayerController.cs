using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float accl;
    public float maxSpeed;
    public float jumpStrength;
    public LayerMask ground;

    private Rigidbody2D rb;
    private EdgeCollider2D feet;
    private float moveX;
    private float moveY;

    private bool canJump;
    private Vector2 jump;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        feet = GetComponent<EdgeCollider2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        this.movementManager();
        this.reset();
	}

    // Helper method that deals with movement.
    private void movementManager() {
        moveX = (rb.velocity.x >= maxSpeed) ? 0 : Input.GetAxis("Horizontal");
        Vector2 forceX = new Vector2(moveX, 0f);
        if (isGrounded()) {
            moveY = Input.GetAxis("Vertical");
            if (moveY > 0) {
                jump = new Vector2(0f, jumpStrength);
                rb.AddForce(jump, ForceMode2D.Impulse);
            }
        }
        // Horizontal movement to player object
        rb.AddForce(forceX * accl);
    }

    #region Helper methods
    // Resets values after processing
    private void reset() {
        
    }
    
    private bool isGrounded() {
        // If its not in the jumping
        if (rb.velocity.y <= 0 && feet.IsTouchingLayers(ground)) {
            Debug.Log(jump.magnitude);
            return true;
        }
        return false;
    }
    #endregion
}
