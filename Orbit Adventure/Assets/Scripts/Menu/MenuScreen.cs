using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using DG.Tweening;
public class MenuScreen : MonoBehaviour
{
    public GameObject ship;

    public void Play()
    {
        ship.GetComponent<MenuShipMove>().enabled = false;
        StartCoroutine(AnimateShip());
    }

    IEnumerator AnimateShip()
    {
        ship.transform.DORotate(ship.transform.eulerAngles + new Vector3(-35, 0, 0), 2f)
            .SetEase(Ease.InOutSine);
        yield return new WaitForSeconds(2f);
        ship.transform.DOMove(new Vector3(-30, -35, 0), 1f)
            .SetEase(Ease.InOutCubic);
        yield return new WaitForSeconds(0.8f);
        SceneManager.LoadScene("InsideShip");
    }
}
