using UnityEngine;
using System.Collections;

namespace Managers{
	public class CoinController : MonoBehaviour {
        public int Level;

        private Animator _anim;
		private GameController _gameController;
        private BoxCollider2D _collider;

		void Start(){
			_gameController = GameController.Instance;
            _anim = GetComponent<Animator>();
            _collider = GetComponent<BoxCollider2D>();
        }

		void OnTriggerEnter2D(Collider2D other){
			if (other.tag == "Player" || other.tag == "Player2") {
				CoinPickup ();
                _collider.enabled = false;
			}
		}

		void CoinPickup(){
			_gameController.AddToken(Level);
            _anim.SetTrigger("Collected_Coin");
            Destroy(this.gameObject, 1);
		}
			

	}

}
