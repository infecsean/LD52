using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemDrop : MonoBehaviour
{
    public float amplitude = 1.0f; // adjust as needed
    public float frequency = 1.0f; // adjust as needed

    public LayerMask groundLayers;
    private Rigidbody2D rb;
    private float hoverY;
    private bool startHover = false;

    private void Start()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 10f, groundLayers);
        hoverY = hit.point.y + 0.1f;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (IsTouchingGround() && startHover == false)
        {
            rb.bodyType = RigidbodyType2D.Static;
            
            startHover = true;
        }

        if (startHover)
        {
            Hover();
        }
            
    }

    private bool IsTouchingGround()
    {
        //Cast a ray downward and check if it hits any colliders in the ground layers
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.2f, groundLayers);
        return hit.collider != null;
    }
    void Hover()
    {
        float yPos = hoverY + amplitude * Mathf.Sin(frequency * Time.time);
        transform.position = new Vector2(transform.position.x, yPos);
    }
}
