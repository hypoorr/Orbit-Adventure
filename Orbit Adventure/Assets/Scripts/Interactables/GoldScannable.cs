using UnityEngine;
using UnityEngine.UI;
public class GoldScannable : Scannable
{

    protected override void Interact()
    {
        IndexManager.goldScanned = true;
    }
}