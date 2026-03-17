using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;
using Unity.VisualScripting;
using TMPro;


public class InventoryItem // inventory item layout
{
    public string itemName;
    public int itemQuantity;
    public bool isTool;
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
    private int currentRow = 0;

    private int currentColumn = 0;

    private TextMeshProUGUI itemNameText;
    private TextMeshProUGUI itemQuantityText;

    public static string equippedItem;


    void Start()
    {

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
    }

    void FixedUpdate()
    {
        for (int i = 0; i < items.Count; i++) // check if resource has 0 quantity and deletes it
        {
            if (items[i].itemQuantity <= 0)
            {
                items.RemoveAt(i);
            }
        }
    }
        public void UpdateInventoryDisplay()
        {
            elementIndex = 0;
            currentRow = 0;
            foreach (GameObject i in itemElementList) // destroy previous inventory elements
            {
                Destroy(i);
            }
            foreach (InventoryItem i in items) // for each inventory item, create a element on the UI for it
            {

                GameObject newElement = Instantiate(itemElement, inventoryBG, true);
                RectTransform rect = newElement.GetComponent<RectTransform>();

                currentRow = Mathf.FloorToInt(elementIndex / 5); // determine which row the element should be on
                currentColumn = elementIndex % 5; // determine the column the element will be in


                itemNameText = newElement.transform.Find("ItemNameText").GetComponent<TMPro.TextMeshProUGUI>();
                itemNameText.text = i.itemName;

                itemQuantityText = newElement.transform.Find("ItemQuantityText").GetComponent<TMPro.TextMeshProUGUI>();
                itemQuantityText.text = i.itemQuantity.ToString() + "x";

                if (i.isTool)
                {
                    newElement.transform.Find("EquipButton").gameObject.SetActive(true);
                    newElement.transform.Find("EquipButton").GetComponent<Button>().onClick.AddListener(delegate {EquipItem(i.itemName); }); // add listener to the button to equip the item
                }


                rect.anchoredPosition = new Vector2(20 * currentColumn - 2.5f, currentRow * -30);

                newElement.SetActive(true);
                newElement.name = i.itemName;
                itemElementList.Add(newElement); // add element to the list
                elementIndex += 1; // increment the index
            }
        }


        public static void AddItem(string addedItemName, int addedItemQuantity, bool addedItemIsTool)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].itemName == addedItemName) // if inventory already contains this, try add to the stack
                {
                    if (items[i].itemQuantity + addedItemQuantity > 99) // if adding this overflows the stack, finish the stack
                    {
                        items[i].itemQuantity = 99;
                    }
                    else // if stack isnt full, add item
                    {
                        items[i].itemQuantity += addedItemQuantity;
                    }
                }
            }

            {

                for (int i = 0; i < items.Count; i++)
                {
                    if (items[i].itemName.Contains(addedItemName))
                    {
                        return;
                    }
                }
                // if inventory doesnt contain new item, add it
                items.Add(new InventoryItem { itemName = addedItemName, itemQuantity = addedItemQuantity, isTool = addedItemIsTool });

            }

        }

        public void EquipItem(string itemEquipped)
        {
            equippedItem = itemEquipped;

            Debug.Log(equippedItem);
        }
}




