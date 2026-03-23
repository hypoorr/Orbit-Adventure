using UnityEngine;
using TMPro;
using System.Runtime.Serialization;
public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI promptText;

    void Start()
    {

    }
    public void UpdateText(string promptMessage)
    {
        promptText.text = promptMessage;
    }
}
