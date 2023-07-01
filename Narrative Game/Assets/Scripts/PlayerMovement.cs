using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private GameObject groundCheck;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask ground;


    Rigidbody2D rb;
    Vector2 input;
    bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        input = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
        isGrounded = Physics2D.OverlapCircle(groundCheck.transform.position, radius, ground);

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
		{
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
		}


    }

	private void FixedUpdate()
	{
        rb.velocity = new Vector2(input.x * speed, rb.velocity.y);
	}
}
