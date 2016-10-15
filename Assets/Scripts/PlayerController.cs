using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Managers;


public class PlayerController : MonoBehaviour {

    public bool facingRight = true;
    public float jumpTime = 0;
    public float jumpDelay = .5f;
    public bool jumped;
    public bool isMoving = false;

    public float accl;
    public float maxSpeed;
    public float jumpStrength;
    public LayerMask[] jumpableLayers;
    public float airCtrl;
	public string displayName = "PLAYER";

    public AudioSource movementAudio;
    public AudioClip playerRunningClip;
    public AudioSource jumpAudio;
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
        _sfxVolume = _controller.GetSFXVolume();
        AudioSource[] aSources = GetComponents<AudioSource>();
        movementAudio = aSources[0];
        movementAudio.clip = playerRunningClip;
        jumpAudio = aSources[1];
        jumpAudio.clip = playerJumpingClip;

    }

    // Update is called once per frame
    void FixedUpdate() {
        this.movementManager();
        this.movementAudioManager();
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

            _anim.SetBool("IsPlayer1", true);

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
                    jumpAudio.Play();
                    // Animation for jump
                    _anim.SetTrigger("Jump");
                }
            } else {
                _airDrag = 1/airCtrl;
            }
        } else if(gameObject.tag == "Player2" && !_ball) {

            _anim.SetBool("IsPlayer1", false);
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
                    jumpAudio.Play();
                    // Animation for jump
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
    private void reset() {

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
        movementAudio.clip = playerRunningClip;
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
            if (!movementAudio.isPlaying)
            {
                movementAudio.Play();
            }
        }
        else
        {
            movementAudio.Stop();
        }
    }
    #endregion
}
