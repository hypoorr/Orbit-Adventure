using UnityEngine;

public class BreakableExample : Breakable
{

    [SerializeField] private string resourceToGrant;

    protected override void Interact()
    {
        Inventory.AddItem(resourceToGrant, 1, false);
        Debug.Log("Interacted with " + gameObject.name);
        Destroy(gameObject);
    }
}
