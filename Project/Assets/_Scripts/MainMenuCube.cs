using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Made by: Tyler J. Sims
// Made on: 12/21/2019
// Made for: DodgeBlock (v3)

public class MainMenuCube : MonoBehaviour
{
    public Vector3 rotSpeed;
    public Vector2 speedClamp;
    // Start is called before the first frame update
    void Start()
    {
        rotSpeed.x = Random.Range(speedClamp.x, speedClamp.y);
        rotSpeed.y = Random.Range(speedClamp.x, speedClamp.y);
        rotSpeed.z = Random.Range(speedClamp.x, speedClamp.y);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotSpeed.x * Time.deltaTime, rotSpeed.y * Time.deltaTime, rotSpeed.z * Time.deltaTime);
    }
}
