using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinTape : ObjMover
{
    public int extraScore = 5; // Eventually will be updated by the spawner
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !FindObjectOfType<GameController>().gameOver)
        {
            FindObjectOfType<GameController>().score += extraScore;
            FindObjectOfType<GameController>().UpdateScore();
            FindObjectOfType<GameController>().EndGame("A winner is you!");
            other.GetComponent<Player>().TakeOff();

        }
    }
}
