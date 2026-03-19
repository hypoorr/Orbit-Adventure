using UnityEngine;

public class ScannableExample : Scannable
{

    protected override void Interact()
    {
        
        Debug.Log("Interacted with " + gameObject.name);
        Destroy(gameObject);
    }
}
