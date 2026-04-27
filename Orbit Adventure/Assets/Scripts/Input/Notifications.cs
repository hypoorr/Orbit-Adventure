using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Notifications : MonoBehaviour
{
    public GameObject notificationTemplate;

    private TextMeshProUGUI titleText;
    private TextMeshProUGUI notiText;

    public AudioSource audioSource;
    public AudioClip notiSound;

    private string givenText;
    private string givenTitle;

    public void NewNotification(string title, string text)
    {

        GameObject newNoti = Instantiate(notificationTemplate, gameObject.transform, true); //create the notification template
        titleText = newNoti.transform.Find("NotiBG").transform.Find("NotiTitle").GetComponent<TMPro.TextMeshProUGUI>(); // find title text
        notiText = newNoti.transform.Find("NotiBG").transform.Find("NotiText").GetComponent<TMPro.TextMeshProUGUI>();// find noti text
        RectTransform rect = newNoti.GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector2(-810.08f, -24.5f); // position the notification

        // assign the given text
        titleText.text = title;
        notiText.text = text;
        newNoti.SetActive(true);
        audioSource.PlayOneShot(notiSound);
        StartCoroutine(DestroyNotification(newNoti));
    }

    IEnumerator DestroyNotification(GameObject notification)
    {

        yield return new WaitForSeconds(5f);
        Destroy(notification);
        // create notification in corner
        // replace titletext with title
        // replace notitext with text
        // disappear after 5 seconds
    }
}
