using System;
using UnityEngine;

public class BreakableExample : Breakable
{

    [SerializeField] private string resourceToGrant;
    private Inventory inventory;

    void Start()
    {

        inventory = FindFirstObjectByType<Inventory>();


    }

    protected override void Interact()
    {
        inventory.AddItem(resourceToGrant, 1, false);
        Debug.Log("Interacted with " + gameObject.name);
        Destroy(gameObject);
    }
}
