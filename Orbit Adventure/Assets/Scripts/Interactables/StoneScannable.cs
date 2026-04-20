using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
public class StoneScannable : Scannable
{
    private Notifications notification;
    void Start()
    {
        notification = GameObject.FindWithTag("Notifications").GetComponent<Notifications>();
    }

    protected override void Interact()
    {
        notification.NewNotification("New Entry", "Stone");
        IndexManager.stoneScanned = true;
    }
}