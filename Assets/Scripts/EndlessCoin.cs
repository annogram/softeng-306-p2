using UnityEngine;
using System.Collections;
using Managers;

public class EndlessCoin : CoinController {

    protected override void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player" || other.tag == "Player2") {
            CoinPickup();
        }
    }

    protected override void CoinPickup() {
        _coinAudio.volume = _gameController.GetSFXVolume();
        _coinAudio.Play();
        this.gameObject.GetComponent<Rigidbody2D>().position = new Vector2(-10, 0);
        //_anim.SetTrigger("Collected_Coin");
    }
}
