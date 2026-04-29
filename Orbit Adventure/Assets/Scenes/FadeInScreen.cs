using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class FadeInScreen : MonoBehaviour
{
    public GameObject fadeUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (SceneManager.GetActiveScene().name != "Main")
        {
            StartCoroutine(FadeOut());
        }

    }

    IEnumerator FadeOut()
    {
        if (SceneManager.GetActiveScene().name == "Main")
        {
            yield return new WaitForSeconds(5f);
        }
        fadeUI.SetActive(true);
        for (int i = 0; i <= 255; i++)
        {
            fadeUI.GetComponent<Image>().color = new Color32(0, 0, 0, (byte)(255 - i));
            yield return new WaitForSeconds(0.01f);
        }

        fadeUI.SetActive(false);
    }

    IEnumerator FadeIn()
    {
        fadeUI.SetActive(true);
        fadeUI.GetComponent<Image>().color = new Color32(0, 0, 0, 0);
        fadeUI.SetActive(true);
        for (int i = 0; i <= 255; i++)
        {
            fadeUI.GetComponent<Image>().color = new Color32(0, 0, 0, (byte)(0 + i));
            yield return new WaitForSeconds(0.01f);
        }
    }


    public void FadeInStart()
    {
        StartCoroutine(FadeIn());
    }

    public void FadeOutStart()
    {
        StartCoroutine(FadeOut());
    }

    // Update is called once per frame
    void Update()
    {

    }
}
