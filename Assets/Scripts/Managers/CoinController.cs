using UnityEngine;
using System.Collections;

namespace Managers{
	public class CoinController : MonoBehaviour {
        public int Level;
        public AudioClip CoinCollectedClip;

        protected Animator _anim;
        protected GameController _gameController;
        protected BoxCollider2D _collider;
        protected AudioSource _coinAudio;

		void Start(){
			_gameController = GameController.Instance;
            _anim = GetComponent<Animator>();
            _collider = GetComponent<BoxCollider2D>();
            _coinAudio = GetComponent<AudioSource>();
            _coinAudio.clip = CoinCollectedClip;
        }

		protected virtual void OnTriggerEnter2D(Collider2D other){
			if (other.tag == "Player" || other.tag == "Player2") {
				CoinPickup ();
                _collider.enabled = false;
			}
		}

		protected virtual void CoinPickup(){
            _coinAudio.volume = _gameController.GetSFXVolume();
            _coinAudio.Play();
			_gameController.AddToken(Level);
            _anim.SetTrigger("Collected_Coin");
            Destroy(this.gameObject, 1);
		}
			

	}

}
