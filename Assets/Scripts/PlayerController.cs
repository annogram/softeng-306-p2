using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float accl;
    public float maxSpeed;
    public float jumpStrength;
    public LayerMask[] jumpableLayers;
    public float airCtrl;

    private bool _ball;
    private Rigidbody2D _rb;
    private EdgeCollider2D _feet;
    private float _moveX;
    private float _moveY;
    private bool _canJump;
    private Vector2 _jump;
    private float _airDrag = 1;

    // Use this for initialization
    void Start() {
        _ball = false;
        _rb = GetComponent<Rigidbody2D>();
        _feet = GetComponent<EdgeCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        this.movementManager();
        this.reset();
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Ramp") {
            _ball = true;
            _feet.enabled = false;
        } else {
            _ball = false;
            _feet.enabled = true;
        }
    }

    void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.tag == "Ramp") {
            _ball = false;
            _feet.enabled = true;
        }
    }

    #region Helper methods
    // Helper method that deals with movement.
    private void movementManager() {
        // Check if we need to do player 1 or player 2 controls
        if (gameObject.tag == "Player" && !_ball) {
            // Horizontal movement
            Vector2 forceX = Vector2.zero;
            if (Input.GetKey(KeyCode.RightArrow)) {
                forceX = new Vector2(1, 0f);
            } else if (Input.GetKey(KeyCode.LeftArrow)) {
                forceX = new Vector2(-1, 0f);
            }
            if (Mathf.Abs(_rb.velocity.x) <= maxSpeed) {
                _rb.AddForce(forceX * (accl * _airDrag));
            }
            if (isGrounded()) {
                _airDrag = 1;
                if (Input.GetKey(KeyCode.UpArrow)) {
                    _jump = new Vector2(0f, jumpStrength);
                    _rb.AddForce(_jump, ForceMode2D.Impulse);
                }
            } else {
                _airDrag = 1/airCtrl;
            }
        } else if(gameObject.tag == "Player2" && !_ball) {
            // Player 2 keys
            Vector2 forceX = Vector2.zero;
            if (Input.GetKey(KeyCode.D)) {
                forceX = new Vector2(1, 0f);
            } else if (Input.GetKey(KeyCode.A)) {
                forceX = new Vector2(-1, 0f);
            }
            if (Mathf.Abs(_rb.velocity.x) <= maxSpeed) {
                _rb.AddForce(forceX * (accl * _airDrag));
            }
            if (isGrounded()) {
                _airDrag = 1;
                if (Input.GetKey(KeyCode.W)) {
                    Vector2 jump = new Vector2(0f, jumpStrength);
                    _rb.AddForce(jump, ForceMode2D.Impulse);
                }
            } else {
                _airDrag = 1 / airCtrl;
            }
        }

        //moveX = (Mathf.Abs(rb.velocity.x) >= maxSpeed) ? 0 : Input.GetAxis("Horizontal");
        //Vector2 forceX = new Vector2(moveX, 0f);
        
        // Horizontal movement to player object

    }

    
    // Resets values after processing
    private void reset() {

    }

    private bool isGrounded() {
        // If its not in the jumping
        if (_rb.velocity.y <= 0) {
            foreach (LayerMask lm in jumpableLayers) {
				if (_feet.IsTouchingLayers (lm)) {
					return true;
				}
            }
        }
        return false;
    }
    #endregion
}
