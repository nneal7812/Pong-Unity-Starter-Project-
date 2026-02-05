using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private TMP_Text playerScoreObject;
    private TMP_Text opponentScoreObject;
    private Ball ball;
    private PlayerPaddle playerPaddle;
    private ComputerPaddle computerPaddle;
    public PlayerInputManager playerManager;
    private int playerScore;
    private int opponentScore;
    private int winningScore = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerScoreObject = GameObject.FindGameObjectWithTag("PlayerScore").GetComponent<TMP_Text>();
        opponentScoreObject = GameObject.FindGameObjectWithTag("OpponentScore").GetComponent<TMP_Text>();
        ball = FindFirstObjectByType<Ball>();
        playerPaddle = FindFirstObjectByType<PlayerPaddle>();
        computerPaddle = FindFirstObjectByType<ComputerPaddle>();

        playerScore = 0;
        opponentScore = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPause()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            SceneManager.LoadScene("PauseScene", LoadSceneMode.Additive);
        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            SceneManager.UnloadSceneAsync("PauseScene");
        }
    }

    public void UpdatePlayerScore()
    {
        playerScore++;
        playerScoreObject.text = playerScore.ToString();

        if (playerScore < winningScore)
        {
            StartCoroutine(ball.ResetBall());
        }
        else
        {
            EndGame();
        }
    }

    public void UpdateOpponentScore()
    {
        opponentScore++;
        opponentScoreObject.text = opponentScore.ToString();

        if (opponentScore < winningScore)
        {
            StartCoroutine(ball.ResetBall());
        }
        else
        {
            EndGame();
        }
    }

    // Freeze game scene and load game over scene
    private void EndGame()
    {
        Debug.Log("Game Over");

        // Make paddles and ball invisible
        playerPaddle.GetComponent<SpriteRenderer>().enabled = false;
        computerPaddle.GetComponent<SpriteRenderer>().enabled = false;
        ball.GetComponent<SpriteRenderer>().enabled = false;

        // Freeze game scene
        Time.timeScale = 0;

        // Load game over scene on top of current scene
        SceneManager.LoadScene("GameOverScene", LoadSceneMode.Additive);
    }
}
