using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Events;

public class Terminal : MonoBehaviour
{
    public TMP_InputField terminalInput;
    public TextMeshProUGUI terminalOutput;
    public GameObject loadingScreen;
    public UnityEvent exitTerminal;

    void Start()
    {
        terminalInput.onEndEdit.AddListener(FinishedTyping);
        terminalOutput.text = "";
        OutputToTerminal("FLIZZYOS");
        OutputToTerminal("Type 'help' for a list of commands");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            exitTerminal.Invoke();
            terminalInput.DeactivateInputField(); // stop typing to the terminal
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void FinishedTyping(string text) // check command typed
    {
        terminalOutput.text = "";
        terminalInput.text = "";
        terminalInput.ActivateInputField();
        OutputToTerminal(">>" + text);

        // CHECK COMMAND
        switch (text.ToLower())
        {
            case "takeoff":
                //takeoff
                StartCoroutine(StartTakeoff());
                break;
            case "help":
                OutputToTerminal("List of commands:");
                OutputToTerminal("Takeoff: if you have enough fuel, ship takes off.");
                OutputToTerminal("FuelCheck: Check your fuel level");
                break;
            case "fuelcheck":
                OutputToTerminal("Current fuel amount: " + ShipFuel.shipFuel.ToString() + "L");
                break;
            default:
                OutputToTerminal("Unknown command. Type 'help' for a list of commands");
                break;

        }
    }

    private void OutputToTerminal(string text)
    {
        terminalOutput.text = terminalOutput.text + "\n" + text;
    }


    IEnumerator StartTakeoff()
    {
        if (SceneManager.GetActiveScene().name == "InsideShip") // if inside the ship, travel to planet
        {
            OutputToTerminal("Taking off...");
            yield return new WaitForSeconds(2f);
            loadingScreen.SetActive(true);
            SceneManager.LoadScene("Main");
        }
        else if (ShipFuel.shipFuel >= 2) // check if player has enough fuel
        {
            if (SceneManager.GetActiveScene().name == "Main") // if on the planet, send back into orbit
            {
                OutputToTerminal("Taking off...");
                yield return new WaitForSeconds(2f);
                SceneManager.LoadScene("InsideShip");
                ShipFuel.shipFuel -= 2;
            }

        }
    }


}
