using UnityEngine;
using System.Collections;

public class ButtonController : MonoBehaviour {
    private Rigidbody2D _rb;
    private bool _pressed;

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
            if (this.GetComponent<Collider2D>().IsTouchingLayers(layer) && !_pressed) {
                _pressed = true;
                this.transform.position = this.transform.position + (Vector3.down * 0.7f);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (_pressed) {
            _pressed = false;
            this.transform.position = this.transform.position + (Vector3.up * 0.7f);
        }
    }
}
