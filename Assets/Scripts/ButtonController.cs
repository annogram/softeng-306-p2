using UnityEngine;
using System.Collections;
using Assets.Scripts;
using Managers;

///<summary>
/// This class is responsible for the button logic for the button game object
///</summary>
public class ButtonController : MonoBehaviour {
    private Rigidbody2D _rb;
    private bool _pressed;
    private AudioSource _buttonAudio;
    private GameController _gameController;

    public GameObject[] ButtonActions;
    public LayerMask[] CanPress;
    public AudioClip ButtonPressClip;

	// Use this for initialization
	void Start () {
        _gameController = GameController.Instance;
        _rb = this.GetComponent<Rigidbody2D>();
        _pressed = false;
        _buttonAudio = GetComponent<AudioSource>();
        _buttonAudio.clip = ButtonPressClip;
	}

    // This method deals with the logic of what happens when the button is pressed
    void OnTriggerEnter2D(Collider2D other) {
        foreach (var layer in CanPress) {
            // If the component that colided with this object is allowed to interact with it then do this
            if (this.GetComponent<Collider2D>().IsTouchingLayers(layer) && !_pressed) {
                _buttonAudio.volume = _gameController.GetSFXVolume();
                _buttonAudio.Play();
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

    // This method deals with the logic of what happens when the button is un pressed
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
