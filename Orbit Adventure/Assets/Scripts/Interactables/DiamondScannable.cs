using UnityEngine;
using UnityEngine.UI;
public class DiamondScannable : Scannable
{

    protected override void Interact()
    {
        IndexManager.diamondScanned = true;
    }
}