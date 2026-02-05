using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody2D ball;
    public Vector2 initialBallPosition;
    public float ballInitialSpeed;
    private float currentSpeed;
    private float maxSpeed = 500f;
    private GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ball = GetComponent<Rigidbody2D>();
        ballInitialSpeed = 5f;
        gameManager = FindFirstObjectByType<GameManager>();

        initialBallPosition = ball.position;

        StartCoroutine(ResetBall());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator ResetBall()
    {
        ball.position = initialBallPosition;

        ball.linearVelocity = Vector2.zero;
        yield return new WaitForSeconds(2);

        ball.linearVelocityX = Random.value > 0.5f ? ballInitialSpeed : -ballInitialSpeed;
        ball.linearVelocityY = Random.Range(-ballInitialSpeed, ballInitialSpeed);
    }

    private void UpdateSpeed(float speedUpdateValue)
    {
        currentSpeed = ball.linearVelocity.magnitude;
        Vector2 direction = ball.linearVelocity.normalized;
        float newSpeed = currentSpeed + speedUpdateValue;
        ball.linearVelocity = direction * newSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (currentSpeed < maxSpeed) { 
            if (collision.gameObject.CompareTag("Paddle"))
            {
                UpdateSpeed(1f);
            }
            else if (collision.gameObject.CompareTag("BounceableWall"))
            {
                UpdateSpeed(.5f);
            }
            else if (collision.gameObject.CompareTag("PlayerWall"))
            {
                gameManager.UpdateOpponentScore();
            }
            else if (collision.gameObject.CompareTag("OpponentWall"))
            {
                gameManager.UpdatePlayerScore();
            }
        }
    }
}
