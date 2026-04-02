using NUnit.Framework;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    private List<Button> buttons;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        buttons = new List<Button>(FindObjectsByType<Button>(FindObjectsSortMode.None));

        foreach (Button button in buttons)
        {
            button.onClick.AddListener(() => OnButtonClick(button));
        }
    }


    private void OnButtonClick(Button button)
    {
        switch (button.name)
        {
            case "SinglePlayerStart":
                SceneManager.LoadScene("GameScene");
                break;
            case "MultiplayerStart":
                SceneManager.LoadScene("MPScene");
                break;
            case "MenuQuit":
                Application.Quit();
                break;
            case "Continue":
                // Check if PauseScene or GameOverScene is loaded and unload accordingly
                if (SceneManager.GetSceneByName("PauseScene").isLoaded)
                {
                    SceneManager.UnloadSceneAsync("PauseScene");
                }
                else if (SceneManager.GetSceneByName("GameOverScene").isLoaded)
                {
                    SceneManager.UnloadSceneAsync("GameOverScene");
                    SceneManager.LoadScene("GameScene");
                }

                Time.timeScale = 1;
                break;
            case "ReturnToMenu":
                // Uses same logic as above (almost)
                if (SceneManager.GetSceneByName("PauseScene").isLoaded)
                {
                    SceneManager.UnloadSceneAsync("PauseScene");
                }
                else if (SceneManager.GetSceneByName("GameOverScene").isLoaded)
                {
                    SceneManager.UnloadSceneAsync("GameOverScene");
                }
                SceneManager.LoadScene("StartScene");
                Time.timeScale = 1;
                break;
            default:
                Debug.Log("No action assigned to button: " + button.name);
                break;
        }
    }
}
