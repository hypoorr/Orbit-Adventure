using UnityEngine;

public class Scanner : MonoBehaviour
{
    public GameObject scannerModel;
    public GameObject scannerText;
    
    void Update()
    {
        if(Inventory.equippedItem == "Scanner")
        {
            scannerModel.SetActive(true);
            scannerText.SetActive(true);

        }
        else
        {
            scannerModel.SetActive(false);
            scannerText.SetActive(false);

            
        }
    }
}
