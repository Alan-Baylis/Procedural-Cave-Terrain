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

		if(jumpTime <= 0)
		{
			canJump = true;
			jumpTime = maxJumpTime;
		}
	}

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
