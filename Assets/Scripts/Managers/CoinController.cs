using UnityEngine;
using System.Collections;

namespace Managers{

	public class CoinController : MonoBehaviour {
        private Animator _anim;

		private GameController _gameController;

		void Start(){
			_gameController = GameController.Instance;
            _anim = GetComponent<Animator>();
        }

		void OnTriggerEnter2D(Collider2D other){
			if (other.tag == "Player" || other.tag == "Player2") {
				CoinPickup ();
			}
		}

		void CoinPickup(){
			_gameController.AddToken ();
            _anim.SetTrigger("Collected_Coin");
            Destroy(this.gameObject, 1);
		}
			

	}

}
