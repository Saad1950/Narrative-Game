using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Windows;

public class EnemyBehaviour : MonoBehaviour
{
	[SerializeField] private float movementSpeed = 5f;
	[SerializeField] private float maxAvoidanceDistance = 1f;

	bool movesRightOrLeft;
	Rigidbody2D rb;
	Vector2 direction;

	private void Start()
	{
		movesRightOrLeft = Utility.RandomTrueOrFalse();
		rb = GetComponent<Rigidbody2D>();

		Physics2D.queriesStartInColliders = false;
	}

	private void FixedUpdate()
	{
		Move();
	}

	private void Update()
	{
		SwitchDirections();
	}

	void SwitchDirections()
	{


		RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, direction, maxAvoidanceDistance);
		if(hitInfo.collider != null && hitInfo.collider.CompareTag("Obstacle"))
		{
			movesRightOrLeft = !movesRightOrLeft;
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.CompareTag("Player"))
		{
			movesRightOrLeft = !movesRightOrLeft;
		}
	}

	void Move()
	{

		if(movesRightOrLeft == true)
		{
			//moveRight
			direction = new Vector2(1f, 0);

		}
		else
		{
			//move left
			direction = new Vector2(-1f, 0);
		}
		Debug.DrawLine(transform.position, (Vector2)transform.position + direction * maxAvoidanceDistance, Color.red);


		rb.velocity = new Vector2(direction.x * movementSpeed, rb.velocity.y);

	}
}
