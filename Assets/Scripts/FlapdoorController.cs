using UnityEngine;
using System.Collections;
using System;

public class FlapdoorController : MonoBehaviour
{
    private PowerupPlayerController ppc;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Console.WriteLine("on trigger entered");
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Player2")
        {
            ppc = col.gameObject.GetComponent<PowerupPlayerController>();
                ppc.becomeBalloon();
        }

            //Some event here

    }

        void OnTriggerExit2D(Collider2D other)
    {
    }

}
