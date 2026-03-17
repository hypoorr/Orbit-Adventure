using UnityEngine;

public class InventoryTest : Interactable
{
    [SerializeField] private Inventory inventory;
    public string itemToGive;


    protected override void Interact()
    {
        Debug.Log("Interacted with " + gameObject.name);
        Inventory.AddItem(itemToGive, 1, false);
    }
}
