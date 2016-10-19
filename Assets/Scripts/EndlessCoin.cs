using UnityEngine;
using System.Collections;
using Managers;

public class EndlessCoin : CoinController {

    private int currentCollected = 0;

    protected override void Start() {
        base.Start();
        currentCollected = 0;
    }

    protected override void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player" || other.tag == "Player2") {
            CoinPickup();
        }
    }

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
