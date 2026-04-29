using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;
public class MenuScreen : MonoBehaviour
{
    public GameObject ship;
    public ParticleSystem particle1;
    public ParticleSystem particle2;
    public GameObject fadeUI;

    public void Play()
    {
        ship.GetComponent<MenuShipMove>().enabled = false;
        gameObject.GetComponent<Button>().enabled = false;
        StartCoroutine(AnimateShip());
    }

    IEnumerator AnimateShip()
    {
        particle1.Stop();
        particle2.Stop();
        yield return new WaitForSeconds(1f);
        ship.transform.DORotate(ship.transform.eulerAngles + new Vector3(-35, 0, 0), 2f)
            .SetEase(Ease.InOutSine);
        yield return new WaitForSeconds(2f);
        particle1.Play();
        particle2.Play();
        ship.transform.DOMove(new Vector3(-30, -35, 0), 1f)
            .SetEase(Ease.InOutCubic);
        StartCoroutine(StartFade());
        yield return new WaitForSeconds(1f);
    }

    IEnumerator StartFade()
    {
        fadeUI.SetActive(true);
        Color color = fadeUI.GetComponent<Image>().color;
        for (int i = 0; i <= 255; i++)
        {
            fadeUI.GetComponent<Image>().color = new Color(0f, 0f, 0f, i * 0.01f);
            yield return new WaitForSeconds(0.01f);
        }
        SceneManager.LoadScene("InsideShip");

    }
}
