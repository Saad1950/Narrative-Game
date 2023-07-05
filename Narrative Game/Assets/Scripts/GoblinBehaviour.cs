using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Windows;

public class GoblinBehaviour : MonoBehaviour
{
	[SerializeField] private float movementSpeed = 5f;
	[Space]
	[Header("Detection Variables")]
	[SerializeField] private float maxAvoidanceDistance = 1f, playerDetectionRange = 2f, yOffset = 0f, xGroundOffset = 2f, maxGroundCheckDistance = 0.5f;
	[Space]
	[Header("Attack Varaiables")]
	[SerializeField] private GameObject projectile;
	[SerializeField] private float projectileForce = 5f;
	[SerializeField] private float pauseTime = 1f;

	bool canMove = true;
	bool playerDetected = false;
	bool hasAttacked = false;
	bool movesRightOrLeft;
	Rigidbody2D rb;
	Vector2 direction;

	Coroutine currentProjectileCoroutine;

	private void Start()
	{
		movesRightOrLeft = Utility.RandomTrueOrFalse();
		rb = GetComponent<Rigidbody2D>();

		Physics2D.queriesStartInColliders = false;
		Physics2D.queriesHitTriggers = false;
	}

	private void FixedUpdate()
	{
		Move();
	}

	private void Update()
	{
		SwitchDirections();
		canMove = !playerDetected;
		DrawRay();

		HandleAttack();
	}

	void SwitchDirections()
	{
		Vector2 rayStartPos = new Vector2(transform.position.x, transform.position.y - yOffset);
		Vector2 groundCheckStartPos = Vector2.zero;

		if (movesRightOrLeft)
		{
			groundCheckStartPos = new Vector2(transform.position.x + xGroundOffset, transform.position.y);

		}
		else
		{
			groundCheckStartPos = new Vector2(transform.position.x - xGroundOffset, transform.position.y);

		}

		RaycastHit2D avoidanceHitInfo = Physics2D.Raycast(rayStartPos, direction, maxAvoidanceDistance);
		RaycastHit2D groundCheck = Physics2D.Raycast(groundCheckStartPos, Vector2.down, maxGroundCheckDistance);
		Debug.DrawLine(groundCheckStartPos, groundCheckStartPos + Vector2.down * maxGroundCheckDistance, Color.yellow);


		if(avoidanceHitInfo.collider != null && avoidanceHitInfo.collider.CompareTag("Obstacle") ||
			groundCheck.collider == null
			)
		{
				movesRightOrLeft = !movesRightOrLeft;
		}

		RaycastHit2D detectionHitInfo = Physics2D.Raycast(rayStartPos, direction, playerDetectionRange);
		if (detectionHitInfo.collider != null && detectionHitInfo.collider.CompareTag("Player"))
		{
			playerDetected = true;
		}
		else
		{
			playerDetected = false;

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
		Debug.DrawLine(transform.position, (Vector2)transform.position + direction * maxAvoidanceDistance, Color.green);

		if(canMove)
		{
			rb.velocity = new Vector2(direction.x * movementSpeed, rb.velocity.y);
		}
		else
		{
			rb.velocity = Vector2.zero;
		}

	}

	void HandleAttack()
	{
		if (playerDetected == true && hasAttacked == false)
		{
			currentProjectileCoroutine = StartCoroutine(ThrowProjectile());
			hasAttacked = true;
		}

		if(playerDetected == false)
		{
			hasAttacked = false;
			if (currentProjectileCoroutine != null)
				StopCoroutine(currentProjectileCoroutine);
		}

	}

	void DrawRay()
	{
		Vector2 rayStartPos = new Vector2(transform.position.x, transform.position.y - yOffset);
		Debug.DrawLine(rayStartPos, (Vector2)rayStartPos + direction * playerDetectionRange, Color.red);
	}


	IEnumerator ThrowProjectile()
	{

		while (true)
		{
			GameObject spawnedProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
			Rigidbody2D projectileRb = spawnedProjectile.GetComponent<Rigidbody2D>();

			if (movesRightOrLeft)
			{
				// projectile moves right
				projectileRb.AddForce(new Vector2(1f, 0f) * projectileForce, ForceMode2D.Impulse);

			}
			else
			{
				//projectile moves left
				projectileRb.AddForce(new Vector2(-1f, 0f) * projectileForce, ForceMode2D.Impulse);

			}
			yield return new WaitForSeconds(pauseTime);
		}

	}
}
