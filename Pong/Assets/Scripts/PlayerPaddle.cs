using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerPaddle : Paddle
{
    private Rigidbody2D playerPaddle;
    private GameManager gameManager;
    private Vector2 moveInput;
    private InputAction moveAction;
    private InputAction pauseAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerPaddle = GetComponent<Rigidbody2D>();
        gameManager = FindFirstObjectByType<GameManager>();

        moveAction = InputSystem.actions.FindAction("PlayerMove");
        pauseAction = InputSystem.actions.FindAction("Pause");
    }

    // Update is called once per frame
    void Update()
    {
        // Handle player input with "Pong Player Movement" action map
        moveInput = moveAction.ReadValue<Vector2>();

        playerPaddle.linearVelocity = new Vector2(0, moveInput.y * paddleSpeed);

        // Checks if player pauses game
        if (pauseAction.WasPressedThisFrame() && Time.timeScale == 1)
        {
            gameManager.OnPause();
        }
        else if (pauseAction.WasPressedThisFrame() && Time.timeScale == 0)
        {
            gameManager.OnPause();
        }
    }
}
