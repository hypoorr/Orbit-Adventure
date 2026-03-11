using UnityEngine;
using System.Collections.Generic;


public class InventoryItem // inventory item layout
{
    public string itemName;
    public int itemQuantity;
}


public class Inventory : MonoBehaviour
{
    public List<InventoryItem> items = new List<InventoryItem>(); // list to hold all items

    void Start()
    {
        items.Add(new InventoryItem {itemName = "Stone", itemQuantity = 1}); // adds an item "Stone" to inventory with 1 quantity
        Debug.Log(items[0].itemName); // will log "Stone"
    }
}




