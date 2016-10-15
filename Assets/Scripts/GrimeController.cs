using UnityEngine;
using System.Collections;
using System.Linq;

public class GrimeController : MonoBehaviour {

    private Rigidbody2D _rb;
    public float Stickiness;

	// Use this for initialization
	void Start () {
        _rb = GetComponent<Rigidbody2D>();

    }
}
