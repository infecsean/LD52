using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public Camera mainCamera;
	public CharacterController2D controller;
	public Animator animator;
	public PlayerStats stats;

	public float runSpeed = 40f;

	float horizontalMove = 0f;
	bool jump = false;

	private GameObject harvestTarget;

	// Update is called once per frame
	void Update()
	{
		Vector2 cursorPos = Input.mousePosition;
		Vector3 worldPos = mainCamera.ScreenToWorldPoint(cursorPos);
		Vector3 targetPos = new Vector3(worldPos.x, worldPos.y, transform.position.z);

		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

		if (Input.GetButtonDown("Jump"))
		{
			jump = true;
		}

		if (Input.GetKeyDown(KeyCode.Mouse0))
        {
			Kill(targetPos);
        }
	}

	void Kill(Vector3 targetPos)
    {
		//Vector3 targetPos = harvestTarget.transform.position;

		Vector2 dashDir = targetPos - transform.position;
		dashDir = new Vector2(dashDir.x, 0);
		controller.Dash(dashDir.normalized);

		if (harvestTarget)
        {
			harvestTarget.GetComponent<NPCMovement>().isAlive = false;
		}
	}

    void FixedUpdate()
	{
		controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
		jump = false;
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Harvestable" && collision.GetComponent<NPCMovement>().isAlive)
        {
			harvestTarget = collision.gameObject;
        }

		if (collision.tag == "OrganDrop" && stats.isBackpackFull == false && collision.GetComponent<OrganItem>().canPickup)
        {
			collision.GetComponent<OrganItem>().Collect(this.gameObject);
			collision.GetComponent<OrganItem>().canPickup = false;
			stats.AddOrgan(collision.gameObject);
		}
    }
}