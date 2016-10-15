using UnityEngine;
using System.Collections;
using System.Linq;

public class GrimeController : MonoBehaviour {

    private Rigidbody2D _rb;
    private Rigidbody2D _playerBody;
    private PlayerController _playerController;
    private bool _triggered;


    public float Stickiness;
    public float OriginalJumpStrength;

	// Use this for initialization
	void Start () {
        _rb = GetComponent<Rigidbody2D>();

    }
	
	void FixedUpdate () {
        if (_triggered)
        {
            this.ApplySlow();
        } 
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" || other.tag == "Player2")
        {
            _playerBody = other.gameObject.GetComponent<Rigidbody2D>();
            _triggered = true;
            _playerBody.velocity = Vector2.zero;
            _playerController = other.GetComponent<PlayerController>();
        }
    }

    void ApplySlow() {
        float playerSpeed = _playerBody.velocity.x;
        _playerController.jumpStrength = 50;
        if (playerSpeed > 0)
        {
            _playerBody.AddForce(Vector2.left * Stickiness);
        }
        else if (playerSpeed < 0)
        {
            _playerBody.AddForce(Vector2.right * Stickiness);
        }

    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player" || other.tag == "Player2")
        {
            _triggered = false;
            _playerController.jumpStrength = OriginalJumpStrength;
            Debug.Log(_playerController.jumpStrength);
        }
    }
}
