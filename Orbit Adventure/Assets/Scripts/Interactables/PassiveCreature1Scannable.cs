using UnityEngine;
using UnityEngine.UI;
public class PassiveCreature1Scannable : Scannable
{

    protected override void Interact()
    {
        IndexManager.flizianScanned = true;
    }
}