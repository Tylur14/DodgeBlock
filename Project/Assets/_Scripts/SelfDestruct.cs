using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Made by: Tyler J. Sims
// Made on: 12/21/2019
// Made for: DodgeBlock (v3)

public class SelfDestruct : MonoBehaviour
{
    public bool timedDestroy = false; // Does this object get destroyed after a certain amount of time?
    public bool randomTime = false;
    public Vector2 ranTimeLimits;
    public float countdownTime = 0.0f; // If so how long will it take?


    void Start()
    {
        CheckSelf();
    }

    public void SetRandTime()
    {
        countdownTime =  Random.Range(ranTimeLimits.x, ranTimeLimits.y);
    }

    public void CheckSelf() // before you wreck yourself
    {
        if (timedDestroy)
            Invoke("DestroySelf", countdownTime);
        else if (randomTime)
        { SetRandTime(); Invoke("DestroySelf", countdownTime); }
    }

    public void DestroySelf()
    {
        Destroy(this.gameObject);
    }
}
