using UnityEngine;

public abstract class Breakable : MonoBehaviour
{
    public string promptMessage;

    public void BaseInteract()
    {
        Interact();
    }
    protected virtual void Interact()
    {
        // template function
    }
}
