using UnityEngine;

public class Craftables : MonoBehaviour
{
    [SerializeField] private Inventory inventory;



    int stone;
    int gold;
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

                inventory.AddItem("Fuel (L)", 4);

            }
            else
            {
                Debug.Log("too broke");
            }
    }

}
