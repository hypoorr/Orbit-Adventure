using UnityEngine;

public class RareItemTest : Interactable
{


    protected override void Interact()
    {
        Debug.Log("Interacted with " + gameObject.name);
        Destroy(gameObject);
    }
}
