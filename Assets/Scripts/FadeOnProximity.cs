using UnityEngine;
using UnityEngine.UI;

public class FadeOnProximity : MonoBehaviour
{
    public float proximityRadius = 5.0f;
    public string playerTag = "Player";
    public float fadeInSpeed = 2.0f;
    private Image image;
    private bool isFadingIn = false;

    void Start()
    {
        image = GetComponent<Image>();
        if (image != null)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
        }
    }

    void Update()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag(playerTag);
        foreach (GameObject player in players)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < proximityRadius)
            {
                isFadingIn = true;
                break;
            }
            else
            {
                isFadingIn = false;
            }
        }
        if (isFadingIn)
        {
            FadeIn();
        }
        else
        {
            FadeOut();
        }
    }

    void FadeIn()
    {
        if (image != null)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, Mathf.Lerp(image.color.a, 1, Time.deltaTime * fadeInSpeed));
        }
    }
    void FadeOut()
    {
        if (image != null)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, Mathf.Lerp(image.color.a, 0, Time.deltaTime * fadeInSpeed));
        }
    }
}
