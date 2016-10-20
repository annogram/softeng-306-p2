using UnityEngine;
using System.Collections;
using System;

///<summary>
/// This class is responsible for the object that interacts with the player to give
/// powerups.
///</summary>

public class PowerupGiverController : MonoBehaviour
{
    private PlayerController ppc;

    //This method gives the player the powerup when they collide with the powerup giver
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Player2")
        {
            ppc = col.gameObject.GetComponent<PlayerController>();
                ppc.isBalloon = true;
        }
    }


}
