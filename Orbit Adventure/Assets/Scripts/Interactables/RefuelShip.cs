using UnityEngine;

public class RefuelShip : Interactable
{
    private int fuelIndex;

    protected override void Interact()
    {
        Debug.Log("Interacted with " + gameObject.name);
        for (int i = 0; i < Inventory.items.Count; i++) // find fuel in inventory
        {
            if(Inventory.items[i].itemName == "Fuel (L)")
            {
                fuelIndex = i;
            }
        }

        Inventory.items[fuelIndex].itemQuantity -= 1; // take away fuel from inventory
        ShipFuel.shipFuel += 1; // add fuel to fuel amount
        
    }
}
