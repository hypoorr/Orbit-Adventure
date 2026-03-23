using UnityEngine;
using UnityEngine.SceneManagement;

public class NewWorldButton : Interactable
{


    protected override void Interact()
    {
        Debug.Log("Interacted with " + gameObject.name);

        if (SceneManager.GetActiveScene().name == "InsideShip") // if inside the ship, travel to planet
        {
            SceneManager.LoadScene("Main");
        }
        else if (ShipFuel.shipFuel >= 2) // check if player has enough fuel
        {
            if(SceneManager.GetActiveScene().name == "Main") // if on the planet, send back into orbit
            {
                SceneManager.LoadScene("InsideShip");
                ShipFuel.shipFuel -= 2;
            }
            
        }
        
    }

    void FixedUpdate()
    {
        if(SceneManager.GetActiveScene().name == "Main")
        {
            promptMessage = "back into orbit (E)\nFuel: " + ShipFuel.shipFuel + "L";
        }
        else
        {
            promptMessage = "Land ship";
        }
    }
}
