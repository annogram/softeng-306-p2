using UnityEngine;
using System.Collections;
using System.Linq;

public class GrimeController : MonoBehaviour, Assets.Scripts.IButtonPress
{

    private Rigidbody2D _rb;
    public float Stickiness;
    public float fallDistance = 3F;
    private bool _player1Inside;
    private bool _player2Inside;
    private Animator _anim;
    private bool isFalling = false;
    private float hasFallen = 0F;

	// Use this for initialization
	void Start () {
        _rb = GetComponent<Rigidbody2D>();
        _player1Inside = false;
        _player2Inside = false;
        _anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (isFalling && (hasFallen < fallDistance))
        {
            this.transform.Translate(Vector2.down);
            hasFallen += 1F;
        }
    }

    public bool Trigger()
    {
        this.isFalling = true;
        return true;
    }

    public bool UnTrigger()
    {
        return true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _player1Inside = true;
            _anim.SetBool("PlayerInside", true);
        } else if (other.tag == "Player2")
        {
            _player2Inside = true;
            _anim.SetBool("PlayerInside", true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _player1Inside = false;
        } else if (other.tag == "Player2")
        {
            _player2Inside = false;
        }

        if (!_player1Inside && !_player2Inside)
        {
            _anim.SetBool("PlayerInside", false);
        }
    }

    public void fall()
    {
        isFalling = true;
    }

}
