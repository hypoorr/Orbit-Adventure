using UnityEngine;
using System.Collections.Generic;


public class InventoryItem // inventory item layout
{
    public string itemName;
    public int itemQuantity;
}


public class Inventory : MonoBehaviour
{
    public GameObject itemElement;
    public static List<InventoryItem> items = new List<InventoryItem>(); // list to hold all items


    private bool inventoryOpen = false;

    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private Transform inventoryBG;
    private List<GameObject> itemElementList = new List<GameObject>();
    private int elementIndex = 0;


    void Start()
    {
        items.Add(new InventoryItem {itemName = "Stone", itemQuantity = 1});
        items.Add(new InventoryItem {itemName = "Fuel (L)", itemQuantity = 5});
        items.Add(new InventoryItem {itemName = "Gold", itemQuantity = 99}); // adds an item "Stone" to inventory with 1 quantity
        Debug.Log(items[0].itemName); // will log "Stone"
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            UpdateInventoryDisplay();
            if (inventoryOpen) // if inventory already open, close it and hide cursor
            {
                inventoryOpen = false;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                inventoryUI.SetActive(false);
            }
            else // if inventory closed, then open it and show cursor
            {
                inventoryOpen = true;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;      
                inventoryUI.SetActive(true);  
            }

        }


        //copy inventory item element
        //locate item name text and set itemname on it
        //locate item quantity text and set itemquantity on it


        void UpdateInventoryDisplay()
        {
            elementIndex = 0;
            foreach(GameObject i in itemElementList) // destroy previous inventory elements
            {
                Destroy(i);
            }
            foreach(InventoryItem i in items) // for each inventory item, create a element on the UI for it
            {
                //Debug.Log(i.itemName + " " + i.itemQuantity);
                GameObject newElement = Instantiate(itemElement,inventoryBG, true); 
                newElement.transform.position = new Vector3(20 * elementIndex, 0, 0);
                itemElementList.Add(newElement); // add element to the list
                elementIndex += 1; // increment the index


                //Debug.Log(items[i].itemName, items[i].itemQuantity);
            }
        }
    }
}




