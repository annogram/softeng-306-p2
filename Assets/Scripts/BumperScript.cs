using UnityEngine;
using System.Linq;
using System.Collections;
using Assets.Scripts;
using System;
using Managers;

public class BumperScript : MonoBehaviour, IButtonPress {

    public float Power;
    public bool OverCharge;
    public float OverChargeMultiplier = 3;
    public AudioClip BumperBumpClip;

    protected Rigidbody2D _rb;
    protected Sprite _currentSprite;
    private Animator _anim;
    private AudioSource _bumperAudio;
    private GameController _gameController;

    // Use this for initialization
    protected virtual void Start () {
        _gameController = GameController.Instance;
        _rb = GetComponent<Rigidbody2D>();
        _currentSprite = GetComponent<Sprite>();
        _anim = GetComponent<Animator>();
        _bumperAudio = GetComponent<AudioSource>();
        _bumperAudio.clip = BumperBumpClip;
    }
    
    void OnCollisionEnter2D(Collision2D other) {
        _bumperAudio.volume = _gameController.GetSFXVolume();
        _bumperAudio.Play();
        _anim.SetTrigger("Hit");
        Rigidbody2D playerBody = other.gameObject.GetComponent<Rigidbody2D>();
        // We need to get the contact points -centre of bumper
        var pointOfContact = other.contacts.FirstOrDefault();
        Vector2 launchTragectory = (OverCharge) 
            ? pointOfContact.normal.normalized * -(Power * OverChargeMultiplier)
            : pointOfContact.normal.normalized * -Power;
        playerBody.AddForce(launchTragectory);
    }

    protected virtual void Update()
    {
        _anim.SetBool("Overcharged", this.OverCharge);
    }


    public virtual bool Trigger() {
        this.OverCharge = !this.OverCharge;
        return true;
    }

    public virtual bool UnTrigger() {
        this.OverCharge = !this.OverCharge;
        return true;
    }
}
