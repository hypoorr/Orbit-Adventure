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
    private bool inventoryOpen = false;
    [SerializeField] private GameObject inventoryUI;
    void Start()
    {
        items.Add(new InventoryItem {itemName = "Stone", itemQuantity = 1}); // adds an item "Stone" to inventory with 1 quantity
        Debug.Log(items[0].itemName); // will log "Stone"
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventoryOpen) // if inventory already open, close it and hide cursor
            {
                inventoryOpen = false;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else // if inventory closed, then open it and show cursor
            {
                inventoryOpen = true;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;        
            }

        }
    }

    //loop through each item in the inventory
    //check the item name and item quantity and display appropriately
    //in the future, have images that display if the object has an image
}




