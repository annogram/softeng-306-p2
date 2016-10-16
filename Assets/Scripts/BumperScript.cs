using UnityEngine;
using System.Linq;
using System.Collections;
using Assets.Scripts;
using System;

public class BumperScript : MonoBehaviour, IButtonPress {

    public float Power;
    public bool OverCharge;
    public float OverChargeMultiplier = 3;

    protected Rigidbody2D _rb;
    protected Sprite _currentSprite;
    private Animator _anim;

	// Use this for initialization
	protected virtual void Start () {
        _rb = GetComponent<Rigidbody2D>();
        _currentSprite = GetComponent<Sprite>();
        _anim = GetComponent<Animator>();
    }
    
    void OnCollisionEnter2D(Collision2D other) {
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
