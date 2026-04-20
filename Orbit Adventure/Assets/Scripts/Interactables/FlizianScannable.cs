using UnityEngine;
using UnityEngine.UI;
public class FlizianScannable : Scannable
{
    private Notifications notification;

    void Start()
    {
        notification = GameObject.FindWithTag("Notifications").GetComponent<Notifications>();
    }
    protected override void Interact()
    {
        notification.NewNotification("New Entry", "Flizian");
        IndexManager.flizianScanned = true;
    }
}