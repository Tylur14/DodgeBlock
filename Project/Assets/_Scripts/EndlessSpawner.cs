using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Made by: Tyler J. Sims
// Made on: 12/17/2019
// Made for: DodgeBlock (v3)

public class EndlessSpawner : MonoBehaviour
{
    public Transform[] spawnPositions;

    public GameObject wallPrefab;
    public float spawnTime = 2.5f;
    public float spawnTimer = 0.0f;


    private void Update()
    {
        if (spawnTimer <= 0)
        {
            SpawnLane();
            spawnTimer = spawnTime;
        }
        else if (spawnTimer > 0)
            spawnTimer -= Time.deltaTime;
    }

    public void SpawnLane()
    {
        List<int> laneIndex = UniqueRandom(0, spawnPositions.Length - 1).ToList();
        int r = UnityEngine.Random.Range(0, 100);

        // There is some weird quirk that makes random favor the set (0,1) and leads to VERY boring levels
        // -- This snippet attempts to fix it and does a moderately (but not perfect) job at it.
        if (r <= 49)
            laneIndex.RemoveAt(laneIndex.Last()); 
        else if(r>=50)
            laneIndex.RemoveAt(laneIndex.First());
        foreach (int i in laneIndex)
            {
                Instantiate(wallPrefab, spawnPositions[i].position, Quaternion.identity, null);
            }
    }

    public IEnumerable<int> UniqueRandom(int minInclusive, int maxInclusive)
    {
        List<int> j = new List<int>();
        for (int i = minInclusive; i <= maxInclusive; i++)
        {
            j.Add(i);
        }
        System.Random rnd = new System.Random();
        while (j.Count > 0)
        {
            int index = rnd.Next(j.Count);
            yield return j[index];
            j.RemoveAt(index);
        }
    }
}
