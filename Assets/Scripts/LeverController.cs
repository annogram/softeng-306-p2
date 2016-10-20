using UnityEngine;
using System.Collections;
using Assets.Scripts;
using Managers;

///<summary>
/// This class is responsible for the logic for the lever objects
///</summary>
public class LeverController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private bool _pressed;
    private Animator _anim;
    private GameController _gameController;
    private AudioSource _leverAudio;

    public GameObject[] ButtonActions;
    public LayerMask[] CanPress;
    public AudioClip LeverSwitchClip;

    // Use this for initialization
    void Start()
    {
        _gameController = GameController.Instance;
        _rb = this.GetComponent<Rigidbody2D>();
        _pressed = false;
        _anim = GetComponent<Animator>();
        _anim.SetBool("Activated", false);
        _leverAudio = GetComponent<AudioSource>();
        _leverAudio.clip = LeverSwitchClip;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Toggle the _pressed boolean state
        _pressed = !_pressed;

        // Set the animation depending on the pressed state
        _anim.SetBool("Activated", _pressed);

        // Audio triggers
        _leverAudio.volume = _gameController.GetSFXVolume();
        _leverAudio.Play();

        // If pressed is true loop through all the connected componets and trigger their
        // on trigger actions
        if (_pressed)
        {
            foreach (var actor in ButtonActions)
            {
                actor.GetComponent<IButtonPress>().Trigger();
            }
        }
        // If pressed is false loop through the connected components and trigger their
        // un trigger actions
        else
        {
            foreach (var actor in ButtonActions)
            {
                actor.GetComponent<IButtonPress>().UnTrigger();
            }
        }

    }
}
