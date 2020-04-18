using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Made by: Tyler J. Sims
// Made on: 12/21/2019
// Made for: DodgeBlock (v3)

// The Obj will control what it does in when spawned / how it interact.
// All this script does is handle the SPAWNING of said Obj.
public class PathedSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public Transform[] spawnPositions;
    public GameObject prefab_Obj; 
    [Space(10)]
    public float spawnTime = 2.5f;
    public float spawnTimer = 0.0f; // Eventually can be set to private

    [Header("Pattern Settings")]
    public Vector3[] pattern;
    public int patternIndex;
    public bool pingPong = false;
    private bool shiftDown = false; // Used for ping pong -- once the index limit is reached it 'shifts down' in the reverse order.

    [Header("Additional Settings")]
    public bool isEndless;
    public GameObject prefab_winTape;
    public int indexToEnd; // ~ Needs renaming

    private void Start()
    {
        patternIndex = 0;
    }

    private void Update()
    {
        TimerTick();
    }

    public void TimerTick()
    {
        if (isEndless || indexToEnd > 0)
        {
            if (spawnTimer <= 0)
            {
                SpawnLane();
                spawnTimer = spawnTime;
                if (!isEndless)
                    indexToEnd--;
            }
            else if (spawnTimer > 0)
                spawnTimer -= Time.deltaTime;
        }
        else if (!isEndless && indexToEnd <= 0)
        { Instantiate(prefab_winTape, spawnPositions[1].position, Quaternion.identity, null); Destroy(this); }

    }

    public void SpawnLane()
    {
        if (pattern[patternIndex].x != 0)
            Instantiate(prefab_Obj, spawnPositions[0].position, Quaternion.identity, null);
        if (pattern[patternIndex].y != 0)
            Instantiate(prefab_Obj, spawnPositions[1].position, Quaternion.identity, null);
        if (pattern[patternIndex].z != 0)
            Instantiate(prefab_Obj, spawnPositions[2].position, Quaternion.identity, null);
        SetIndex();
    }

    public void SetIndex()
    {
        if (pingPong)
        {
            if (!shiftDown)
            {
                patternIndex++;
                if(patternIndex > pattern.Length - 1)
                {
                    patternIndex = pattern.Length - 2;
                    shiftDown = true;
                }
            }
            else if (shiftDown)
            {
                patternIndex--;
                if (patternIndex < 0)
                {
                    patternIndex = 1;
                    shiftDown = false;
                }
            }
        }
        else
        {
            patternIndex++;
                if (patternIndex > pattern.Length - 1)
                    patternIndex = 0;
        }
    }
}