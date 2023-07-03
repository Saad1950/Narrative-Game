using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CakeSpawner : MonoBehaviour
{
	[SerializeField] private GameObject[] cakePrefabs;
	[SerializeField] private Transform cakeSpawnLevelTransform;
	[SerializeField] private Transform leftBorderTransform, rightBorderTransform;
	[SerializeField] private Transform middlePlatform;
	[SerializeField] private float timeBetweenSpawns = 5f, moveSpeed;
	float ySpawnPos;

	Coroutine moveCakeCoroutine;
	

	private void Start()
	{
		ySpawnPos = cakeSpawnLevelTransform.transform.position.y;

		StartCoroutine(SpawnCakes());
	}

	IEnumerator SpawnCakes()
	{
		while(true)
		{
			bool randomBool = Utility.RandomTrueOrFalse();

			if (randomBool)
			{
				SpawnCake(rightBorderTransform, randomBool);

			}
			else
			{
				SpawnCake(leftBorderTransform, randomBool);
			}


			yield return new WaitForSeconds(timeBetweenSpawns);
		}
	}

	void SpawnCake(Transform border, bool randomBool)
	{
		int randomCakeValue = Random.Range(0, cakePrefabs.Length);

		GameObject spawnedCake = Instantiate(cakePrefabs[randomCakeValue], Vector2.one, Quaternion.identity);

		Rigidbody2D rb = spawnedCake.GetComponent<Rigidbody2D>();
		BoxCollider2D collider = spawnedCake.GetComponent<BoxCollider2D>();

		float xValue = 0f;
		float directionValue = 0f;

		if(!randomBool)
		{
			xValue = border.position.x - (spawnedCake.transform.localScale.x / 2f);
			directionValue = 1f;


		}
		else if(randomBool)
		{
			xValue = border.position.x + (spawnedCake.transform.localScale.x / 2f);
			directionValue = -1f;

		}

		Vector2 spawnPos = new Vector2(xValue, ySpawnPos);
		spawnedCake.transform.position = spawnPos;

		Vector2 translation = new Vector2(directionValue, 0f);


		moveCakeCoroutine = StartCoroutine(MoveCake(translation, rb, collider));




		

	}

	IEnumerator MoveCake(Vector2 translation, Rigidbody2D rb, BoxCollider2D collider)
	{
		while(!Input.GetKeyDown(KeyCode.Space))
		{
			rb.transform.Translate(translation * moveSpeed * Time.deltaTime);
			yield return null;
		}
		rb.gravityScale = 1f;
		collider.enabled = true;


	}

}	
