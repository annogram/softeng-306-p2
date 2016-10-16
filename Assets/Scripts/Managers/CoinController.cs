using UnityEngine;
using System.Collections;

namespace Managers{
	public class CoinController : MonoBehaviour {
        public int Level;
        public AudioClip coinCollectedClip;

        private Animator _anim;
		private GameController _gameController;
        private BoxCollider2D _collider;
        private AudioSource _coinAudio;

		void Start(){
			_gameController = GameController.Instance;
            _anim = GetComponent<Animator>();
            _collider = GetComponent<BoxCollider2D>();
            _coinAudio = GetComponent<AudioSource>();
            _coinAudio.clip = coinCollectedClip;
        }

		void OnTriggerEnter2D(Collider2D other){
			if (other.tag == "Player" || other.tag == "Player2") {
				CoinPickup ();
                _collider.enabled = false;
			}
		}

		void CoinPickup(){
            _coinAudio.volume = _gameController.GetSFXVolume();
            _coinAudio.Play();
			_gameController.AddToken(Level);
            _anim.SetTrigger("Collected_Coin");
            Destroy(this.gameObject, 1);
		}
			

	}

}
