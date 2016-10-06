using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class ButtonController : MonoBehaviour {
    private Rigidbody2D _rb;
    private bool _pressed;

    public GameObject[] ButtonActions;
    public LayerMask[] CanPress;
	// Use this for initialization
	void Start () {
        _rb = this.GetComponent<Rigidbody2D>();
        _pressed = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other) {
        foreach (var layer in CanPress) {
            // If the component that colided with this object is allowed to interact with it then do this
            if (this.GetComponent<Collider2D>().IsTouchingLayers(layer) && !_pressed) {
                _pressed = true;
                this.transform.position = this.transform.position + (Vector3.down * 0.7f);
                // Now that we know the button has been pressed we will go through all the objects
                // Which need to be triggered
                foreach (var actor in ButtonActions) {
                    actor.GetComponent<IButtonPress>().Trigger();
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (_pressed) {
            _pressed = false;
            this.transform.position = this.transform.position + (Vector3.up * 0.7f);
            // Now that we know the button has been pressed we will go through all the objects
            // Which need to be triggered
            foreach (var actor in ButtonActions) {
                actor.GetComponent<IButtonPress>().UnTrigger();
            }
        }
    }
}
