using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Made by: Tyler J. Sims
// Made on: 12/21/2019
// Made for: DodgeBlock (v3)

public class Wall : ObjMover
{
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            other.GetComponent<Player>().TakeDamage();
    }
}
