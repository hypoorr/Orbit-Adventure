using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class TerminalInteract : Interactable
{
    public GameObject player;
    public TMP_InputField inputField;
    public GameObject loadingScreen;
    public UnityEvent onInteract;


    protected override void Interact()
    {
        inputField.ActivateInputField();
        onInteract.Invoke();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }
}
