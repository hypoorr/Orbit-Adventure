using UnityEngine;

public class Notifications : MonoBehaviour
{
    public GameObject notificationTemplate;

    static private void NewNotification(string title, string text)
    {
        GameObject newElement = Instantiate(notificationTemplate, gameObject.transform, true);

        // create notification in corner
        // replace titletext with title
        // replace notitext with text
        // disappear after 5 seconds
    }
}
