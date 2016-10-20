using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Managers;

///<summary>
/// This class is responsible for the player logic
public class PlayerController : MonoBehaviour {

    public bool facingRight = true;
    public float jumpTime = 0;
    public float jumpDelay = .5f;
    public bool jumped;
    public bool isMoving = false;
    public bool isBalloon = false;

    public float accl;
    public float maxSpeed;
    public float jumpStrength;
    public LayerMask[] jumpableLayers;
    public float airCtrl;
	  public string displayName = "PLAYER";

    public AudioClip playerRunningClip;
    public AudioClip playerJumpingClip;

    private bool _ball;
    private Rigidbody2D _rb;
    private EdgeCollider2D _feet;
    private float _moveX;
    private float _moveY;
    private bool _canJump;
    private Vector2 _jump;
    private float _airDrag = 1;
    private Animator _anim;
	  private Canvas _name;
    private GameController _controller;
    private float _sfxVolume;
    private float _player1Speed;
    private float _player2Speed;
    private float floatGravity = -20;

    private AudioSource _movementAudio;
    private AudioSource _jumpAudio;

    private float _originalJumpStrength;
    private GrimeController _grimeController;
    private bool _inGrime;

    private bool isTouchingPlayer = false;

    // Use this for initialization
    void Start() {
        _ball = false;
        _rb = GetComponent<Rigidbody2D>();
        _feet = GetComponent<EdgeCollider2D>();
        _anim = GetComponent<Animator>();
		    _name = GetComponentInChildren<Canvas> ();
		    setPlayerName (displayName);
        _controller = GameController.Instance;
        AudioSource[] aSources = GetComponents<AudioSource>();
        _movementAudio = aSources[0];
        _movementAudio.clip = playerRunningClip;
        _jumpAudio = aSources[1];
        _jumpAudio.clip = playerJumpingClip;
        _originalJumpStrength = jumpStrength;
    }

    // Update is called once per frame
    void FixedUpdate() {
        _sfxVolume = _controller.GetSFXVolume();
        this.movementManager();
        this.movementAudioManager();
        this.HandleLayers();
		    this.reset();

        // Checks if player is in grime and applies the slow effect on the player
        if (_inGrime)
        {
            this.ApplySlow();
        }
    }

    // This method deals with the collision entry with ramps and other player models
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

    // This method deals with the collision exit with ramps and other player models
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

    #region Grime Methods
    // This method sets all the appropriate variables when the player enters the grime
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Grime")
        {
            _inGrime = true;
            _rb.velocity = Vector2.zero;
            _grimeController = other.GetComponent<GrimeController>();
            _anim.SetBool("InGrime", true);
        }
    }

    // This method applies the slow effect on the player by setting the jumpStrength
    // and a force to slow down the player
    void ApplySlow()
    {
        float playerSpeed = _rb.velocity.x;
        jumpStrength = 50;
        if (playerSpeed > 0)
        {
            _rb.AddForce(Vector2.left * _grimeController.Stickiness);
        }
        else if (playerSpeed < 0)
        {
            _rb.AddForce(Vector2.right * _grimeController.Stickiness);
        }

    }

    // This method sets all the appropriate variables when players exit the grime
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Grime")
        {
            jumpStrength = _originalJumpStrength;
            _inGrime = false;
            _anim.SetBool("InGrime", false);
        }
    }
    #endregion


    #region Helper methods
    // Helper method that deals with movement.
    private void movementManager() {

        // Updates the speed parameter in the animator to animate the walk
        _player1Speed = Input.GetAxis("Player1Horizontal");
        _anim.SetFloat("Speed1", Mathf.Abs(_player1Speed));
        _player2Speed = Input.GetAxis("Player2Horizontal");
        _anim.SetFloat("Speed2", Mathf.Abs(_player2Speed));

        if (_rb.velocity.y < 0)
        {
            _anim.SetBool("Land", true);
        }

        // Check if we need to do player 1 or player 2 controls
        if (gameObject.tag == "Player" && !_ball) {

            bool skin1 = _controller._player1Skin == SkinColour.BLUE ? false : true;
            _anim.SetBool("IsAltSkin", skin1);
            if (isBalloon) {
                _rb.gravityScale = floatGravity;
            } else {
                _rb.gravityScale = 20F;
            }

            // Horizontal movement
            Vector2 forceX = Vector2.zero;
            if (Input.GetKey(KeyCode.RightArrow)) {
                forceX = new Vector2(1, 0f);
                if (_player1Speed > 0 && !facingRight)
                {
                    Flip();
                }

            } else if (Input.GetKey(KeyCode.LeftArrow)) {
                forceX = new Vector2(-1, 0f);
                if (_player1Speed < 0 && facingRight)
                {
                    Flip();
                }

            }
            if (Mathf.Abs(_rb.velocity.x) <= maxSpeed) {
                _rb.AddForce(forceX * (accl * _airDrag));
            }
            if (isGrounded()) {
                _airDrag = 1;
                if (Input.GetKey(KeyCode.UpArrow)) {
                    _jump = new Vector2(0f, jumpStrength);
                    _rb.AddForce(_jump, ForceMode2D.Impulse);
                    _jumpAudio.volume = _sfxVolume;
                    _jumpAudio.Play();
                    // Animation for jump
                    _anim.SetTrigger("Jump");
                }
            } else if (!isBalloon) {
                _airDrag = 1 / airCtrl;
            }
        } else if(gameObject.tag == "Player2" && !_ball) {

            bool skin2 = _controller._player2Skin == SkinColour.RED ? false : true;
            _anim.SetBool("IsAltSkin", skin2);
            if (isBalloon) {
                _rb.gravityScale = floatGravity;
            } else {
                _rb.gravityScale = 20F;
            }

            // Player 2 keys
            Vector2 forceX = Vector2.zero;
            if (Input.GetKey(KeyCode.D)) {
                forceX = new Vector2(1, 0f);
                if (_player2Speed > 0 && !facingRight)
                {
                    Flip();
                }
            } else if (Input.GetKey(KeyCode.A)) {
                forceX = new Vector2(-1, 0f);
                if (_player2Speed < 0 && facingRight)
                {
                    Flip();
                }
            }
            if (Mathf.Abs(_rb.velocity.x) <= maxSpeed) {
                _rb.AddForce(forceX * (accl * _airDrag));
            }
            if (isGrounded()) {
                _airDrag = 1;
                if (Input.GetKey(KeyCode.W)) {
                    Vector2 jump = new Vector2(0f, jumpStrength);
                    _rb.AddForce(jump, ForceMode2D.Impulse);
                    _jumpAudio.volume = _sfxVolume;
                    _jumpAudio.Play();
                    // Animation for jump
                    _anim.SetTrigger("Jump");
                }
            } else if (!isBalloon) {
                _airDrag = 1 / airCtrl;
            }
        }




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
		if (!facingRight) {
			_name.transform.localScale = new Vector3 (-1, 1f, 1f);
		} else {
			_name.transform.localScale = new Vector3 (1, 1f, 1f);
		}
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    // Resets values after processing
    void reset() {

    }

	void setPlayerName(string name){
		displayName = name;
		_name.GetComponentInChildren<Text> ().text = displayName;
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

    private void movementAudioManager()
    {
        _movementAudio.clip = playerRunningClip;
        if (_player1Speed != 0 || _player2Speed != 0)
        {
            this.isMoving = true;
        }
        else
        {
            this.isMoving = false;
        }
        if (this.isMoving && isGrounded())
        {
            if (!_movementAudio.isPlaying)
            {
                _movementAudio.volume = _sfxVolume;
                _movementAudio.Play();
            }
        }
        else
        {
            _movementAudio.Stop();
        }
    }
    #endregion
}
