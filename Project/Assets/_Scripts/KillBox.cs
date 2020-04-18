using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Made by: Tyler J. Sims
// Made on: 12/17/2019
// Made for: DodgeBlock (v3)

public class KillBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }
}
