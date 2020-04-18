using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Made by: Tyler J. Sims
// Made on: 12/17/2019
// Made for: DodgeBlock (v3)

public class Player : MonoBehaviour
{
    [Range(0, 2)]
    public int health;
    public float shieldDuration;
    private float shieldTimer;

    public Color[] colors;
    [Range(0,2)]
    public int colorIndex;
    private MeshRenderer M_rend;
    void Start()
    {
        M_rend = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        M_rend.material.color = Color.Lerp(M_rend.material.color, colors[colorIndex], Time.deltaTime * 2);
        if (shieldTimer > 0)
            shieldTimer -= Time.deltaTime;
    }
    
    public void TakeDamage()
    {
        if(shieldTimer <= 0)
        {
            if (health > 0)
            {
                colorIndex--;
                shieldTimer = shieldDuration;
            }
            else if (health <= 0)
                Die();
            health--;
        }
        
        
    }

    public void Die()
    {
        print("Player has died!");
        FindObjectOfType<GameController>().EndGame("Game Over... You failure...");
        Destroy(this.gameObject);
    }

    public void TakeOff() // The bit where the player shoots off after WINNING a round
    {
        GetComponent<PlayerInput>().enabled = false;
        this.enabled = false;
        GetComponent<ObjMover>().enabled = true;
    }
}
