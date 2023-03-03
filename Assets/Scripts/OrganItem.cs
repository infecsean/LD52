using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrganItem : MonoBehaviour
{
    // This script is attached onto every item drop and it needs to
    // - Hover the sprite over the ground for the given organ
    // - Remember the tool used to harvest the organ
    // - Start the expiration countdown when instantiated
    // - Destroy itself and add itself to player's inventory when in pickup range

    public OrganObject baseOrgan;
    public float lifeLeft = 9999;
    public ToolObjects toolUsed;
    public float priceAppliedTool;

    public bool canPickup = false;

    private void Start()
    {
        canPickup = false;
        GetComponent<SpriteRenderer>().sprite = baseOrgan.organSprite;
        priceAppliedTool = toolUsed.ergonomics * toolUsed.quality * toolUsed.sharpness * baseOrgan.marketPrice;
        lifeLeft = baseOrgan.preserveTime;
        StartCoroutine(DropCooldown(1f));
    }
    void Update()
    {
        lifeLeft -= Time.deltaTime;
        if (lifeLeft < 0)
        {
            SelfDestruct();
        }
    }

    public void Collect(GameObject obj)
    {
        if (canPickup == true)
        {
            StartCoroutine(MoveObject(obj, 0.5f));
        }
    }

    IEnumerator DropCooldown(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        canPickup = true;
    }

    IEnumerator MoveObject(GameObject obj, float duration)
    {
        Vector2 startPosition = transform.position;
        float startTime = Time.time;
        while (Time.time < startTime + duration)
        {
            float t = (Time.time - startTime) / duration;
            transform.position = Vector2.Lerp(startPosition, obj.transform.position, t);
            yield return null;
        }
        transform.position = obj.transform.position;
        //gameObject.SetActive(false);
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
    }

    void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
