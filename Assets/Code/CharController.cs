using UnityEngine;
using System.Collections;

public class CharController : MonoBehaviour {

    Rigidbody2D rb;
    public float moveSpeed;
    float moveX;
    public float jumpForce;


	// Use this for initialization
	void Start () 
    {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        moveX = Input.GetAxis("Horizontal") * moveSpeed;

        if(Input.GetButtonDown("Jump"))
        {
            print("JUMP");
            rb.AddRelativeForce(new Vector2(0, jumpForce));
        }

	}

    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveX, 0);
    
    }


}
