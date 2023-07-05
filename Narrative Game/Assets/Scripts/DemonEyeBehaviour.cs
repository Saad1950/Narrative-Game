using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonEyeBehaviour : MonoBehaviour
{
	[SerializeField] private Transform point1, point2;
	[SerializeField] private GameObject projectile;
	[SerializeField] private float projectileForce = 5f, timeBetweenShots = 2f;
	[SerializeField] private float movementSpeed = 5f;

	float current = 0f, target = 1f;
	bool isMoving = true;
	bool hasDetected = false;

	LayerMask demonDetectables;

	Rigidbody2D rb;

	Coroutine currentAttackCoroutine;

	private void Start()
	{
		transform.position = point1.position;
		rb = GetComponent<Rigidbody2D>();
		StartCoroutine(Move());

		Physics2D.queriesHitTriggers = false;
		Physics2D.queriesStartInColliders = false;
	}

	private void FixedUpdate()
	{
		Move();
		DetectPlayer();
	}

	void DetectPlayer()
	{
		RaycastHit2D hitInfo = Physics2D.BoxCast(transform.position, transform.localScale / 8f, 90f, Vector2.down);
		ExtDebug.DrawBoxCastBox(transform.position, transform.localScale / 8f, Quaternion.Euler(0f, 0f, 90f), Vector2.down, hitInfo.distance, Color.red);

		//RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 1000f, demonDetectables);

		if (hitInfo.collider != null)
		{
			if(hitInfo.collider.CompareTag("Player"))
			{
				if(hasDetected == false)
				{
					currentAttackCoroutine = StartCoroutine(Attack());
					isMoving = false;
					hasDetected = true;
				}
			}
			else
			{
				if(currentAttackCoroutine != null)
					StopCoroutine(currentAttackCoroutine);
				isMoving = true;
				hasDetected = false;
			}
			

		}
	}

	IEnumerator Attack()
	{
		while(true)
		{
			GameObject spawnedProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
			Rigidbody2D projectileRb = spawnedProjectile.GetComponent<Rigidbody2D>();
			projectileRb.AddForce(Vector2.down * projectileForce, ForceMode2D.Impulse);
			yield return new WaitForSeconds(timeBetweenShots);
		}

	}

	IEnumerator Move()
	{
		Vector2 position1 = point1.position;
		Vector2 position2 = point2.position;
		
		while(true)
		{
			if(isMoving)
			{
				current = Mathf.MoveTowards(current, target, movementSpeed * Time.fixedDeltaTime);
				rb.position = Vector2.Lerp(position1, position2, current);
				if (rb.position == position2)
				{
					current = 1f;
					target = 0f;

				}
				else if (rb.position == position1)
				{
					current = 0f;
					target = 1f;
				}
			}
			yield return new WaitForFixedUpdate();
		}
	}
}
