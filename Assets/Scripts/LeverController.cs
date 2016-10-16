using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class LeverController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private bool _pressed;

    public GameObject[] ButtonActions;
    public LayerMask[] CanPress;
    private Animator _anim;
    // Use this for initialization
    void Start()
    {
        _rb = this.GetComponent<Rigidbody2D>();
        _pressed = false;
        _anim = GetComponent<Animator>();
        _anim.SetBool("Activated", false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        _pressed = !_pressed;
        _anim.SetBool("Activated", _pressed);

        if (_pressed)
        {
            foreach (var actor in ButtonActions)
            {
                actor.GetComponent<IButtonPress>().Trigger();
            }
        }
        else
        {
            foreach (var actor in ButtonActions)
            {
                actor.GetComponent<IButtonPress>().UnTrigger();
            }
        }

    }
}
