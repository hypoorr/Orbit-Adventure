using UnityEngine;
using UnityEngine.UI;
public class PassiveCreature1Scannable : Scannable
{
    private Notifications notification;

    void Start()
    {
        notification = GameObject.FindWithTag("Notifications").GetComponent<Notifications>();
    }

    protected override void Interact()
    {
        notification.NewNotification("New Entry", "Wizzledizzle");
        IndexManager.passiveCreature1Scanned = true;
    }
}