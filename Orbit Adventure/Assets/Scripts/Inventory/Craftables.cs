using UnityEngine;

public class Craftables : MonoBehaviour
{
    [SerializeField] private Inventory inventory;



    int stone;
    int gold;
    int diamond;

    public void CraftFuel()
    {
        for (int i = 0; i < Inventory.items.Count; i++)
        {
            if (Inventory.items[i].itemName == "Stone") // get the index for stone
            {
                stone = i;
            }
            if (Inventory.items[i].itemName == "Gold") // get the index for gold
            {
                gold = i;
            }
            
        }

            if (Inventory.items[stone].itemQuantity >= 2 && Inventory.items[gold].itemQuantity >= 1) // if requirements are met, remove resource and grant item
            {
                Debug.Log("Crafting fuel");
                Inventory.items[stone].itemQuantity -= 2;
                Inventory.items[gold].itemQuantity -= 1;

                Inventory.AddItem("Fuel (L)", 4, false);

            }
            else
            {
                Debug.Log("too broke");
            }
    }

   public void CraftScanner()
    {
        for (int i = 0; i < Inventory.items.Count; i++)
        {
            if (Inventory.items[i].itemName == "Diamond")
            {
                diamond = i;
            }
            if (Inventory.items[i].itemName == "Gold")
            {
                gold = i;
            }
            
        }

            if (Inventory.items[diamond].itemQuantity >= 1 && Inventory.items[gold].itemQuantity >= 3) // if requirements are met, remove resource and grant item
            {
                Debug.Log("Crafting fuel");
                Inventory.items[diamond].itemQuantity -= 1;
                Inventory.items[gold].itemQuantity -= 3;

                Inventory.AddItem("Scanner", 1, true); // true means the item is a tool

            }
            else
            {
                Debug.Log("too broke");
            }
    }

}
