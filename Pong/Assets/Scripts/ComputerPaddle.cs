using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;

public class ComputerPaddle : Paddle
{
    private Rigidbody2D computerPaddle;
    private Rigidbody2D ball;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        computerPaddle = GetComponent<Rigidbody2D>();
        ball = GameObject.FindGameObjectWithTag("Ball").GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void FixedUpdate()
    {
        float currentY;

        if (ball.linearVelocityX > 0)
        {
            currentY = ball.position.y;
        }
        else
        {
            currentY = 0;
        }

        float newY = Mathf.MoveTowards(computerPaddle.position.y, currentY, paddleSpeed * Time.fixedDeltaTime);
        computerPaddle.position = new Vector2(computerPaddle.position.x, newY);
    }
}
