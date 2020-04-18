using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

// Made by: Tyler J. Sims
// Made on: 12/21/2019
// Made for: DodgeBlock (v3)

public class MainMenuController : MonoBehaviour
{
    public int score;
    public TextMeshPro display_Score;
    private void Start()
    {
        score = PlayerPrefs.GetInt("Score");
        display_Score.text = score.ToString();
    }

    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, -Vector3.up, out hit, 100.0f))
            {
                if (hit.collider.gameObject.tag == "Cube_Button")
                    hit.collider.gameObject.GetComponent<Cube_Button>().PressButton();
            }
                
        }
    }
    public void PlayGame(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
