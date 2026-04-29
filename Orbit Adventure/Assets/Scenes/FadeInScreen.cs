using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeInScreen : MonoBehaviour
{
    public GameObject fadeUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        for (int i = 0; i <= 255; i++)
        {
            fadeUI.GetComponent<Image>().color = new Color(0f, 0f, 0f, i * -0.01f);
            yield return new WaitForSeconds(0.01f);
        }

        fadeUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
