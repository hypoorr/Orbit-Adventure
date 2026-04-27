using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MenuSensSetting : MonoBehaviour
{
    public Slider sensSlider;
    public TextMeshProUGUI sensitivityText;

    // Update is called once per frame
    void Update()
    {
        if (sensSlider)
        {
            PlayerLook.xSensitivity = sensSlider.value;
            PlayerLook.ySensitivity = sensSlider.value;
            sensitivityText.text = "Sensitivity: " + (sensSlider.value).ToString();
        }

    }
}
