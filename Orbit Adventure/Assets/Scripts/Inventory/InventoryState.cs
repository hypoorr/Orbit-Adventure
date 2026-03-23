using UnityEngine;

public class InventoryState : MonoBehaviour
{
    [SerializeField] private string currentState;
    
    public GameObject itemList;
    public GameObject craftablesList;
    public GameObject indexButtons;
    public GameObject stoneInfo;
    public GameObject diamondInfo;
    public GameObject shipInfo;
    public GameObject goldInfo;

    [SerializeField] private Inventory inventory;

    public void SetState(string state)
    {
        currentState = state;
        inventory.UpdateInventoryDisplay();
    }

    void FixedUpdate()
    {
        if (currentState == "Crafting")
        {
            if(itemList)
            {
                itemList.SetActive(false);
                craftablesList.SetActive(true);
                indexButtons.SetActive(false);
                diamondInfo.SetActive(false);
                stoneInfo.SetActive(false);
                shipInfo.SetActive(false);
                goldInfo.SetActive(false);
            }

        }

        if (currentState == "Inventory")
        {
            if(itemList)
            {
                itemList.SetActive(true);
                craftablesList.SetActive(false);
                indexButtons.SetActive(false);
                diamondInfo.SetActive(false);
                stoneInfo.SetActive(false);
                shipInfo.SetActive(false);
                goldInfo.SetActive(false);
            }
        }

        if (currentState == "Index")
        {
            itemList.SetActive(false);
            craftablesList.SetActive(false);
            indexButtons.SetActive(true);
            diamondInfo.SetActive(false);
            stoneInfo.SetActive(false);
            shipInfo.SetActive(false);
            goldInfo.SetActive(false);
        }
        if (currentState == "StoneInfo")
        {
            itemList.SetActive(false);
            craftablesList.SetActive(false);
            indexButtons.SetActive(false);
            stoneInfo.SetActive(true);
        }
        if (currentState == "DiamondInfo")
        {
            itemList.SetActive(false);
            craftablesList.SetActive(false);
            indexButtons.SetActive(false);
            diamondInfo.SetActive(true);
        }
        if (currentState == "ShipInfo")
        {
            itemList.SetActive(false);
            craftablesList.SetActive(false);
            indexButtons.SetActive(false);
            shipInfo.SetActive(true);
        }
        if (currentState == "GoldInfo")
        {
            itemList.SetActive(false);
            craftablesList.SetActive(false);
            indexButtons.SetActive(false);
            goldInfo.SetActive(true);
        }
    }
}
