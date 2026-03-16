using UnityEngine;

public class InventoryState : MonoBehaviour
{
    [SerializeField] private string currentState;
    
    public GameObject itemList;
    public GameObject craftablesList;
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
            }

        }

        if (currentState == "Inventory")
        {
            if(itemList)
            {
                itemList.SetActive(true);
                craftablesList.SetActive(false);
            }
        }

        if (currentState == "Index")
        {
            itemList.SetActive(false);
            craftablesList.SetActive(false);
        }
    }
}
