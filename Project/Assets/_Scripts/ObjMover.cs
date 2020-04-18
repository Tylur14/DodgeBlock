using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Made by: Tyler J. Sims
// Made on: 12/17/2019
// Made for: DodgeBlock (v3)

public class ObjMover : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float dir = 1.0f;
    private Vector3 vel = Vector3.zero;
    void Update()
    {
        var pos = transform.position;
        pos.z -= (moveSpeed *dir)* Time.deltaTime;
        
        //transform.position = pos;
        transform.position = Vector3.SmoothDamp(transform.position,pos,ref vel,Time.deltaTime);
    }
    
}
