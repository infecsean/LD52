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
    
    void Start()
    {
        StartCoroutine(MoveRandomly());
    }

    void Update()
    {
        if (!isAlive)
        {
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
            controller.Move(runSpeed * Random.Range(-10f, 10f) * Random.Range(1f, 2f), false, false) ;
            yield return new WaitForSeconds(moveInterval);
        }
    }

}
