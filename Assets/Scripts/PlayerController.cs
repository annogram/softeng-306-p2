using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public bool facingRight = true;
    public float jumpTime = 0;
    public float jumpDelay = .5f;
    public bool jumped;

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
    private Animator _anim;

    private bool isTouchingPlayer = false;

    // Use this for initialization
    void Start() {
        _ball = false;
        _rb = GetComponent<Rigidbody2D>();
        _feet = GetComponent<EdgeCollider2D>();
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        this.movementManager();
        this.HandleLayers();
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

        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Player2")
        {
            isTouchingPlayer = true;
        }
    }

    void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.tag == "Ramp") {
            _ball = false;
            _feet.enabled = true;
        }
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Player2")
        {
            isTouchingPlayer = false;
        }
    }

    public bool IsTouchingPlayer()
    {
        return isTouchingPlayer;
    }

    #region Helper methods
    // Helper method that deals with movement.
    private void movementManager() {

        // Updates the speed parameter in the animator to animate the walk
        float speed = Input.GetAxis("Horizontal");
        _anim.SetFloat("Speed", Mathf.Abs(speed));

        // Deals with flippin the player left or right
        if (speed > 0 && !facingRight)
            Flip();
        else if (speed < 0 && facingRight)
            Flip();

        if(_rb.velocity.y < 0)
        {
            _anim.SetBool("Land", true);
        }

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

                    // Animation for jump
                    _anim.SetTrigger("Jump");
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

                    _anim.SetTrigger("Jump");
                }
            } else {
                _airDrag = 1 / airCtrl;
            }
        }

        //moveX = (Mathf.Abs(rb.velocity.x) >= maxSpeed) ? 0 : Input.GetAxis("Horizontal");
        //Vector2 forceX = new Vector2(moveX, 0f);
        
        // Horizontal movement to player object

    }

    // Handles animator Layers
    private void HandleLayers()
    {
        if (! this.isGrounded())
        {
            _anim.SetLayerWeight(1, 1);
        }
        else
        {
            _anim.SetLayerWeight(1, 0);
        }
    }

    // Flips the player's orientation
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }


    // Resets values after processing
    private void reset() {

    }

    private bool isGrounded() {
        // If its not in the jumping
        if (_rb.velocity.y <= 0) {
            foreach (LayerMask lm in jumpableLayers) {
				if (_feet.IsTouchingLayers (lm)) {

                    _anim.ResetTrigger("Jump");
                    _anim.SetBool("Land", false);

					return true;
				}
            }
        }
        return false;
    }
    #endregion
}
