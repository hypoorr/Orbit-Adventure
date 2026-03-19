using UnityEngine;
using UnityEngine.UI;
public class StoneScannable : Scannable
{

    protected override void Interact()
    {
        IndexManager.stoneScanned = true;
    }
}