using UnityEngine;
using UnityEngine.UI;
public class IndexManager : MonoBehaviour
{
    public static bool stoneScanned = false;
    public GameObject stoneIndex;

    public static bool diamondScanned = false;
    public GameObject diamondIndex;

    public static bool goldScanned = false;
    public GameObject goldIndex;

    public static bool flizianScanned = false;
    public GameObject flizianIndex;

    void Update()
    {
        if (stoneScanned)
        {
            Transform undiscoveredText = stoneIndex.transform.GetChild(2);
            undiscoveredText.gameObject.SetActive(false);
            Transform infoButton = stoneIndex.transform.GetChild(0);
            Transform name = stoneIndex.transform.GetChild(1);
            infoButton.gameObject.SetActive(true);
            name.gameObject.SetActive(true);
        }
        if (diamondScanned)
        {
            Transform undiscoveredText = diamondIndex.transform.GetChild(2);
            undiscoveredText.gameObject.SetActive(false);
            Transform infoButton = diamondIndex.transform.GetChild(0);
            Transform name = diamondIndex.transform.GetChild(1);
            infoButton.gameObject.SetActive(true);
            name.gameObject.SetActive(true);
        }
        if (goldScanned)
        {
            Transform undiscoveredText = goldIndex.transform.GetChild(2);
            undiscoveredText.gameObject.SetActive(false);
            Transform infoButton = goldIndex.transform.GetChild(0);
            Transform name = goldIndex.transform.GetChild(1);
            infoButton.gameObject.SetActive(true);
            name.gameObject.SetActive(true);
        }
        if (flizianScanned)
        {
            Transform undiscoveredText = flizianIndex.transform.GetChild(2);
            undiscoveredText.gameObject.SetActive(false);
            Transform infoButton = flizianIndex.transform.GetChild(0);
            Transform name = flizianIndex.transform.GetChild(1);
            infoButton.gameObject.SetActive(true);
            name.gameObject.SetActive(true);
        }
    }
}
