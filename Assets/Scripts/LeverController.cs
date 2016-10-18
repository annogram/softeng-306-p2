using UnityEngine;
using System.Collections;
using Assets.Scripts;
using Managers;

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

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        _pressed = !_pressed;
        _anim.SetBool("Activated", _pressed);
        _leverAudio.volume = _gameController.GetSFXVolume();
        _leverAudio.Play();
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
