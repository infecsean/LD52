using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public float runSpeed = 1f;
    public float moveInterval = 2f;
    public bool isAlive = true;
    public Sprite deadSprite;

    public List<OrganObject> organObjects;
    public PlayerStats stats;

    // ChatGPT Scatter
    public List<GameObject> organPrefabs; // Prefab to scatter
    public Transform scatterArea; // Area to scatter the prefabs in
    public float scatterRadius = 5.0f; // Radius of the scatter area
    public float scatterSpeed = 2.0f; // Speed at which the prefabs are scattered
    public float hoverSpeed = 2.0f; // Speed at which the object hovers

    public LayerMask groundLayer; // LayerMask to detect ground
    public float groundCheckDistance = 10.0f; // Distance to check for ground

    private bool isInstantiated = false;
    // ChatGPT end

    void Start()
    {
        StartCoroutine(MoveRandomly());
    }

    void Update()
    {
        if (!isAlive)
        {
            if (!isInstantiated)
            {
                Scatter();
                isInstantiated = true;
            }
            GetComponent<Animator>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().sprite = deadSprite;
            gameObject.GetComponent<Collider2D>().enabled = false;
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            
            StopAllCoroutines();
        }
    }

    IEnumerator MoveRandomly()
    {
        while(true)
        {
            controller.Move(runSpeed * Random.Range(-10f, 10f) * Random.Range(1f, 2f), false) ;
            yield return new WaitForSeconds(moveInterval);
        }
    }

    // ChatGPT
    private void Scatter()
    {
        foreach (GameObject obj in organPrefabs)
        {
            if (Random.Range(0f, 1f) > obj.GetComponent<OrganItem>().baseOrgan.successRate)
            {
                Vector3 scatterPosition = scatterArea.position;
                Quaternion scatterRotation = Quaternion.Euler(0, 0, Mathf.Lerp(80, 100, Random.value));
                Vector2 scatterDirection = scatterRotation * Vector2.right;
                GameObject scatteredPrefab = Instantiate(obj, scatterPosition, scatterRotation);
                scatteredPrefab.GetComponent<OrganItem>().toolUsed = stats.currentTool;
                Rigidbody2D rb = scatteredPrefab.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.AddForce(scatterDirection * scatterSpeed * Random.Range(0.9f, 1f), ForceMode2D.Impulse);
                }
            }
        }
    }

    

}
