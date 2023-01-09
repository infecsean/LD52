using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

	public CharacterController2D controller;
	public Animator animator;
	public PlayerStats stats;

	public float runSpeed = 40f;
	public float leapSpeed = 40;

	float horizontalMove = 0f;
	bool jump = false;

	private bool harvestable = false;
	private GameObject harvestTarget;

	// Update is called once per frame
	void Update()
	{

		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

		if (Input.GetButtonDown("Jump"))
		{
			jump = true;
		}

		if (Input.GetKeyDown(KeyCode.Mouse0) && harvestable)
        {
			Kill();
        }
	}

	void Kill()
    {
		float leapDir = harvestTarget.transform.position.x - transform.position.x;
		controller.Move(leapSpeed*leapDir, false, false);

		harvestTarget.GetComponent<NPCMovement>().isAlive = false;
		foreach (OrganObject organ in harvestTarget.GetComponent<NPCMovement>().organObjects)
        {
			
			if (Random.Range(0f,1f) > organ.successRate) // if rolled to a success,
            {
				stats.AddOrgan(organ);
				Debug.Log("Added organ: " + organ.organName);
			}
			else
            {
				continue;
            }
        }
	}

	void FixedUpdate()
	{
		controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
		jump = false;
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Harvestable")
        {
			harvestTarget = collision.gameObject;
			harvestable = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Harvestable")
        {
            harvestable = false;
        }
    }
}