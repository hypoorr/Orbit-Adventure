using UnityEngine;

public class InventoryTest : Interactable
{
    [SerializeField] private Inventory inventory;
    public string itemToGive;


    protected override void Interact()
    {
        Debug.Log("Interacted with " + gameObject.name);
        inventory.AddItem(itemToGive, 1);
    }
}
