using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
public class StoneScannable : Scannable
{
    private Notification notification;
    void Start()
    {

    }

    protected override void Interact()
    {
        notification.NewNotification("New item", "Gained " + addedItemName + " " + addedItemQuantity.ToString() + "x");
        IndexManager.stoneScanned = true;
    }
}