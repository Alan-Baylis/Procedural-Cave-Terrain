using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
	public float speed;
	public float maxJumpTime;

	private float jumpForce = 250f;
	private float move = 0f;
	private float jumpTime = 0f;
	private bool canJump;

	void Start()
	{
		jumpTime = maxJumpTime;
	}

	void Update()
	{
		if(!canJump)
		{
			jumpTime -= Time.deltaTime;
		}

        //A better way to establish whether or not the player can jump is to check if it is colliding with the ground. 
        //Mark all the ground with a 'ground' tag and then check for collision with that tag. 
		if(jumpTime <= 0)
		{
			canJump = true;
			jumpTime = maxJumpTime;
		}
	}

    /*Like this. Note: you will need to set the tags in the inspector
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "ground")
        {
            canJump=True;
        }
    }*/


	void FixedUpdate()
	{
		if(Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
		{
			move = 0f;
		}
		else if(Input.GetKey(KeyCode.A))
		{
			move = -1f;
		}
		else if(Input.GetKey(KeyCode.D))
		{
			move = 1f;
		}
		else
		{
			move = 0f;
		}

		GetComponent<Rigidbody2D>().velocity = new Vector2(move * speed, GetComponent<Rigidbody2D>().velocity.y);

		if(Input.GetKey(KeyCode.Space) && canJump)
		{
			GetComponent<Rigidbody2D>().AddForce(new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpForce));
			canJump = false;
			jumpTime = maxJumpTime;
		}
	}
}
