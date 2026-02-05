using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MenuUIController : MonoBehaviour
{
    [Header("Optional: Assign a default button")]
    public GameObject firstSelected;

    private GameObject lastSelected;

    void OnEnable()
    {
        if (firstSelected != null)
        {
            EventSystem.current.SetSelectedGameObject(firstSelected);
            lastSelected = firstSelected;
        }
    }

    void Update()
    {
        var eventSystem = EventSystem.current;
        var gamepad = Gamepad.current;

        if (eventSystem.currentSelectedGameObject == null && lastSelected != null)
        {
            eventSystem.SetSelectedGameObject(lastSelected);
        }

        if (eventSystem.currentSelectedGameObject != null && gamepad != null)
        {
            lastSelected = eventSystem.currentSelectedGameObject;
        }
    }
}