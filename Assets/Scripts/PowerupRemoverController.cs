using UnityEngine;
using System.Collections;
using System;

///<summary>
/// This class is responsible for the object that interacts with the player to remove
/// powerups.
///</summary>

public class PowerupRemoverController : MonoBehaviour
{
    private PlayerController ppc;

    //This method removies the powerup when the player collides with the powerup remover
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Player2")
        {
            ppc = col.gameObject.GetComponent<PlayerController>();
                ppc.isBalloon = false;
        }
    }
}
