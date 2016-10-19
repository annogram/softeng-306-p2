using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

namespace Managers{
    /// <summary>
    /// Controls the behavior of the coin game object
    /// </summary>
	public class CoinController : MonoBehaviour {
        public int Level;
        public AudioClip CoinCollectedClip;

        protected Animator _anim;
        protected GameController _gameController;
        protected BoxCollider2D _collider;
        protected AudioSource _coinAudio;
		private GameObject _scoreHUD;

		protected virtual void Start(){
			_gameController = GameController.Instance;
            _anim = GetComponent<Animator>();
            _collider = GetComponent<BoxCollider2D>();
            _coinAudio = GetComponent<AudioSource>();
            _coinAudio.clip = CoinCollectedClip;
			_scoreHUD = GameObject.FindGameObjectWithTag ("ScoreHUD");

        }

		protected virtual void OnTriggerEnter2D(Collider2D other){
			if (other.tag == "Player" || other.tag == "Player2") {
				CoinPickup ();
                _collider.enabled = false;
			}
		}

        /// <summary>
        /// CoinPickup triggers incimenting the cached token counter
        /// the method has been made virtrual so that <code>EndlessCoin</code> can
        /// modify behavior appropriately.
        /// 
        /// <seealso cref="Managers.EndlessCoin"/>
        /// </summary>
		protected virtual void CoinPickup(){
            _coinAudio.volume = _gameController.GetSFXVolume();
            _coinAudio.Play();
			_gameController.AddToken(Level);
            _anim.SetTrigger("Collected_Coin");
			updateScore ();
            // Removes the coin after a certian delay so the animation can play through
            Destroy(this.gameObject, 1);
		}

        /// <summary>
        /// Updates the score in the hud
        /// </summary>
		protected void updateScore() {

			Transform score = _scoreHUD.transform.Find ("Score");
			Text scoreText = score.GetComponent<Text> ();
			int newScore = Int32.Parse(scoreText.text) + 1;
			scoreText.text = newScore.ToString ();

		}
			

	}

}
