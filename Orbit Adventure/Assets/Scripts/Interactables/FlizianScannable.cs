using UnityEngine;
using UnityEngine.UI;
public class FlizianScannable : Scannable
{

    protected override void Interact()
    {
        IndexManager.flizianScanned = true;
    }
}