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
        if (!(terminalInput.text == ""))
        {
            terminalInput.text = "";
            if(gameObject.transform.Find("TerminalCamera").gameObject.activeSelf)
            {
                terminalOutput.text = "";
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
                        OutputToTerminal("Planetscan: gives information about the planet");
                        break;


                    case "fuelcheck":
                        OutputToTerminal("Current fuel amount: " + ShipFuel.shipFuel.ToString() + "L");
                        OutputToTerminal("You can craft more fuel then refuel around the side of the ship");
                        break;


                    case "hi":
                        OutputToTerminal("hey");
                        break;


                    case "planetscan":
                        StartCoroutine(PlanetScan());
                        break;


                    default:
                        OutputToTerminal("Unknown command. Type 'help' for a list of commands");
                        break;

                }
            }

        

        }
        if(gameObject.transform.Find("TerminalCamera").gameObject.activeSelf)
        {
            terminalInput.ActivateInputField();
        }
        else
        {
            terminalInput.DeactivateInputField();
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

    IEnumerator PlanetScan()
    {
        if (SceneManager.GetActiveScene().name == "InsideShip")
        {
            OutputToTerminal("You must be landed on a planet to do this!");
        }
        else
        {
            OutputToTerminal("Scanning...");
            for(int i = 0; i < Random.Range(3,5); i++)
            {
                yield return new WaitForSeconds(0.5f);
                OutputToTerminal("...");
            }
            OutputToTerminal("PLANET NAME: " + TerrainGenerator.planetName);
            OutputToTerminal("");
            OutputToTerminal("SEED: " + TerrainGenerator.seed.ToString());
            OutputToTerminal("");
            OutputToTerminal("INTELLIGENT LIFE FOUND: " + TerrainGenerator.hasEnemies.ToString());
            OutputToTerminal("");
            OutputToTerminal("RESOURCES FOUND:");
            for (int i = 0; i < TerrainGenerator.resourcesPresent.Count; i++) // log all resources present in world
            {
                OutputToTerminal(TerrainGenerator.resourcesPresent[i]);
            }
        }   
    }


}
