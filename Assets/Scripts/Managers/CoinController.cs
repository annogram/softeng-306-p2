using UnityEngine;
using System.Collections;

namespace Managers{

	public class CoinController : MonoBehaviour {

		private GameController _gameController;

		void Start(){
			_gameController = GameController.Instance;
		}

		void OnTriggerEnter2D(Collider2D other){
			if (other.tag == "Player" || other.tag == "Player2") {
				CoinPickup ();
			}
		}

		void CoinPickup(){
			_gameController.AddToken ();
			Destroy (this.gameObject);
		}
			

	}

}
