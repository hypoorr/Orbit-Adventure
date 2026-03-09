using UnityEngine;

public class BlockScript : Interactable
{


    protected override void Interact()
    {
        Debug.Log("Interacted with " + gameObject.name);
        Destroy(gameObject);
    }
}
