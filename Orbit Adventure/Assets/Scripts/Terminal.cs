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

    public FadeInScreen fadeInScreen;

    static private string inputtedName = "";

    void Start()
    {
        terminalInput.onEndEdit.AddListener(FinishedTyping);
        terminalOutput.text = "";
        if (inputtedName == "") //check if its their first time using the terminal
        {
            OutputToTerminal("FLIZZYOS");
            OutputToTerminal("New user detected! Please state your name:");
        }
        else
        {
            OutputToTerminal("Welcome, " + inputtedName);
            OutputToTerminal("Type 'help' for a list of commands");
        }
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
            if (gameObject.transform.Find("TerminalCamera").gameObject.activeSelf)
            {
                terminalOutput.text = "";
                OutputToTerminal(">>" + text);

                if (inputtedName == "") //if they havent set a name, treat command as name input
                {
                    inputtedName = text;
                    OutputToTerminal("Welcome, " + inputtedName);
                    OutputToTerminal("");
                    OutputToTerminal("This terminal has been repurposed in an attempt to keep you alive while travelling.");
                    OutputToTerminal("FlizCorp™ are not responsible for any fatal problems, namely death, that may occur during\nusage.");
                    OutputToTerminal("");
                    OutputToTerminal("Type 'help' for a list of commands");
                }
                else //if name exists, use normal commands
                {
                    // CHECK COMMAND
                    switch (text.ToLower())
                    {
                        case "takeoff":
                            //takeoff
                            StartCoroutine(StartTakeoff());
                            break;


                        case "help":
                            OutputToTerminal("Press 'i' at any time to open the inventory");
                            OutputToTerminal("List of commands:");
                            OutputToTerminal("Takeoff: if you have enough fuel, ship takes off.");
                            OutputToTerminal("FuelCheck: Check your fuel level");
                            OutputToTerminal("Planetscan: gives information about the planet");
                            OutputToTerminal("rng: output a random number");
                            OutputToTerminal("");
                            OutputToTerminal("");
                            OutputToTerminal("");
                            OutputToTerminal("");
                            OutputToTerminal("");
                            OutputToTerminal("");
                            OutputToTerminal("EMPLOYEE USAGE ONLY");
                            OutputToTerminal("Debugger: Assess the ship");
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

                        case "rng":
                            OutputToTerminal(Random.Range(1, 11).ToString());
                            break;

                        case "debugger":
                            OutputToTerminal("Currently rented to: [REDACTED]");
                            OutputToTerminal("Stability: 1%");
                            OutputToTerminal("ESTIMATED SURVIVAL CHANCE: 0%");
                            OutputToTerminal("");
                            OutputToTerminal("");
                            OutputToTerminal("");
                            OutputToTerminal("");
                            OutputToTerminal("");
                            OutputToTerminal("");
                            OutputToTerminal("FlizzyOS by FlizCorp™");
                            break;

                        case "i":
                            OutputToTerminal("Press i when not on the terminal silly...");

                            break;

                        default:
                            OutputToTerminal("Unknown command. Type 'help' for a list of commands");
                            break;

                    }
                }

            }



        }
        if (gameObject.transform.Find("TerminalCamera").gameObject.activeSelf)
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
            fadeInScreen.FadeInStart();
            yield return new WaitForSeconds(3f);
            loadingScreen.SetActive(true);
            SceneManager.LoadScene("Main");
        }
        else if (ShipFuel.shipFuel >= 2) // check if player has enough fuel
        {
            if (SceneManager.GetActiveScene().name == "Main") // if on the planet, send back into orbit
            {
                OutputToTerminal("Taking off...");
                fadeInScreen.FadeInStart();
                yield return new WaitForSeconds(2f);
                ShipFuel.shipFuel -= 2;
                SceneManager.LoadScene("InsideShip");
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
            for (int i = 0; i < Random.Range(3, 5); i++)
            {
                yield return new WaitForSeconds(0.5f);
                OutputToTerminal("...");
            }
            OutputToTerminal("PLANET NAME: " + TerrainGenerator.planetName);
            OutputToTerminal("");
            OutputToTerminal("SEED: " + TerrainGenerator.seed.ToString());
            OutputToTerminal("");
            OutputToTerminal("AGGRESSIVE LIFE FOUND: " + TerrainGenerator.hasEnemies.ToString());
            OutputToTerminal("");
            if (TerrainGenerator.passiveEvent == true)
            {
                OutputToTerminal("CAUTION: UNUSUAL SWARMS OF LIFE DETECTED");
                OutputToTerminal("");
            }
            OutputToTerminal("RESOURCES FOUND:");
            for (int i = 0; i < TerrainGenerator.resourcesPresent.Count; i++) // log all resources present in world
            {
                OutputToTerminal(TerrainGenerator.resourcesPresent[i]);
            }
        }
    }


}
