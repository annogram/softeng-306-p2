using UnityEngine;
using System.Collections;
using Managers;

///<summary>
/// This class is respnsible for the logic of coins in the endless game mode
///</summary>

public class EndlessCoin : CoinController {

    private int currentCollected = 0;

    // This method is for initialization
    protected override void Start() {
        base.Start();
        currentCollected = 0;
    }

    // This method calls the coinpickup method when a player model touches it
    protected override void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player" || other.tag == "Player2") {
            CoinPickup();
        }
    }

    // This method is responsible or all the actions when a coin is picked up
    protected override void CoinPickup() {
        _coinAudio.volume = _gameController.GetSFXVolume();
        _coinAudio.Play();
        this.gameObject.GetComponent<Rigidbody2D>().position = new Vector2(-10, 0);
        currentCollected++;
        _gameController.UpdateEndlessHighscore(currentCollected);
        updateScore();
        //_anim.SetTrigger("Collected_Coin");
    }
}
