using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LinkPlayerMovement : MonoBehaviour
{
	[Header("Movement Variables")]
	[SerializeField] private float groundSpeed = 5f, airSpeedModifier = 0.6f;
	float actualSpeed;
	[SerializeField] private float jumpForce = 5f;
	[SerializeField] private GameObject groundCheck;
	[SerializeField] private float radius;
	[SerializeField] private LayerMask ground;
	[SerializeField] private Transform rayStart;
	[Space]
	[Header("Combat Variables")]
	[SerializeField] private float maxHitDistance;

	SpriteRenderer spriteRenderer;
	Rigidbody2D rb;
	Vector2 input;
	bool isGrounded;
	bool isFacingRight = true;

	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();

		Physics2D.queriesStartInColliders = false;
	}

	// Update is called once per frame
	void Update()
	{
		input = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
		isGrounded = Physics2D.OverlapCircle(groundCheck.transform.position, radius, ground);

		if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
		{
			Jump();
		}

		ChangeSpeedInAir();

		if ((input.x > 0f && !isFacingRight) || (input.x < 0 && isFacingRight))
		{
			print("flipped");
			FlipSprite();
		}

		RayCast();

	}

	void ChangeSpeedInAir()
	{
		float newFastSpeed = groundSpeed *= airSpeedModifier;
		float newSlowSpeed = groundSpeed /= airSpeedModifier;

		if(!isGrounded)
		{
			actualSpeed = newFastSpeed;
		}
		else
		{
			actualSpeed = newSlowSpeed;
		}

	}

	void RayCast()
	{
		Vector2 direction = Vector2.right;
		if(isFacingRight)
		{
			direction = Vector2.right;
		}
		else
		{
			direction = Vector2.left;
		}


		Attack(direction); 

		
	}

	void Attack(Vector2 direction)
	{
		RaycastHit2D hitInfo = Physics2D.Raycast(rayStart.position, direction, maxHitDistance);
		
		Debug.DrawLine(rayStart.position, (Vector2)rayStart.position +  direction * maxHitDistance, Color.red);

		if (Input.GetKeyDown(KeyCode.Mouse0))
		{

			if(hitInfo.collider != null && hitInfo.collider.CompareTag("Enemy"))
			{
				if (hitInfo.collider.GetComponent<Health>() != null)
				{
					hitInfo.collider.GetComponent<Health>().numOfHearts--;
				}
			}

		}

	}


	void Jump()
	{
		rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
	}

	private void FlipSprite()
	{
		isFacingRight = !isFacingRight;
		spriteRenderer.flipX = !isFacingRight;
	}

	private void FixedUpdate()
	{
		rb.velocity = new Vector2(input.x * actualSpeed, rb.velocity.y);
	}

}
