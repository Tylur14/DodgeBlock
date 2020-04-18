using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

// Made by: Tyler J. Sims
// Made on: 12/17/2019
// Made for: DodgeBlock (v3)

public class GameController : MonoBehaviour
{
    [Header("End Game")]
    public GameObject gameOverObj;
    public TextMeshProUGUI gameOverMsg;
    public bool gameOver = false;

    [Header("Scoring")]
    public TextMeshPro display_score;
    public TextMeshProUGUI newHighScore;
    public int score;
    private int maxScore = 99999;

    private float scoreTimer = 1;

    [Header("Tutorial")] // Simple instructions at the start of (?)every round
    public bool inTutorial = true;
    public bool onDesktop = false;
    public TextMeshPro display_tutorialPrompt;
    public float startingTimeScale = 0.35f;
    void Start()
    {
        CheckDevice();
        Time.timeScale = startingTimeScale;
        
    }

    void Update()
    {
        if (!gameOver)
        {
            UpdateScore();
            CheckTutorial();
        }
    }

    public void CheckDevice()
    {
        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            display_tutorialPrompt.text = "Press 'W' or 'A' to move! (Bumpers on a joypad)";
            onDesktop = true;
        }
        else if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            display_tutorialPrompt.text = "Tap on either side of the screen to move!";
            onDesktop = false;
        }
    }

    public void UpdateScore()
    {
        if (score < maxScore && scoreTimer <= 0)
        { score++; scoreTimer = 1.0f; }
        else if (score < maxScore && scoreTimer > 0)
            scoreTimer -= Time.deltaTime;

        display_score.text = score.ToString();
    } // Could be thrown to it's own class

    public void CheckTutorial()
    {
        if (!inTutorial)
        {
            display_tutorialPrompt.color = Color.Lerp(display_tutorialPrompt.color, Color.clear, Time.deltaTime * 4f);
        }

        if (onDesktop && (Input.GetButtonDown("GoRight") || Input.GetButtonDown("GoLeft")))
            TurnOffTutorial();
    } // Could be thrown to it's own class

    public void TurnOffTutorial()
    {
        if (inTutorial)
            inTutorial=false;
        Time.timeScale = 1.0f;
    }

    public void EndGame(string message) // Game is over and the player can no longer move (is dead) HOWEVER this active scene is still the play field -- bring up UI to restart or return to main menu
    {
        TurnOffTutorial();
        gameOver = true;
        gameOverMsg.text = message;
        Destroy(GameObject.FindGameObjectWithTag("Spawner"));
        Time.timeScale = startingTimeScale;
        var walls = FindObjectsOfType<SelfDestruct>();
        foreach(SelfDestruct wall in walls)
        {
            wall.randomTime = true;
            wall.CheckSelf();
        }
        gameOverObj.SetActive(true);
        if (score > PlayerPrefs.GetInt("Score"))
        { PlayerPrefs.SetInt("Score", score); newHighScore.enabled = true; }
        int tempTScore = PlayerPrefs.GetInt("TotalScore") + score;
        PlayerPrefs.SetInt("TotalScore", tempTScore);
    }

    public void RestartGame() // Restart this current game with the same parameters
    {
        var thisScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(thisScene.ToString(), LoadSceneMode.Single);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);

    }
    //public void QuitGame()
    //{
    //    Application.Quit();
    //}
}
