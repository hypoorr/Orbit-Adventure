using UnityEngine;

public class Scanner : MonoBehaviour
{
    public GameObject scannerModel;
    
    void Update()
    {
        if(Inventory.equippedItem == "Scanner")
        {
            scannerModel.SetActive(true);

        }
        else
        {
            scannerModel.SetActive(false);
            
        }
    }
}
