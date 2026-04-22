using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;
public class BinaryMonitor : MonoBehaviour
{
    public TextMeshProUGUI binaryText;
    private string generatedText;
    void Start()
    {
        StartCoroutine(createText());
    }

    IEnumerator createText()
    {
        while (true)
        {
            generatedText = "";

            for (int x = 0; x < 11; x++) // repeat for each line
            {
                for (int i = 0; i < 55; i++) // length of 1 line
                {
                    generatedText = generatedText + Random.Range(0,2).ToString();
                }
                generatedText = generatedText + "\n";
            }
            binaryText.text = generatedText;

            yield return new WaitForSeconds(Random.Range(1f, 1.5f));
        }

    }
}
