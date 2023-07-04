using UnityEngine;
using System.Collections;

public class FallingBlocksSpawner : MonoBehaviour
{
	[SerializeField] private GameObject fallingBlockPrefab;
	[SerializeField] private Transform leftBorder, rightBorder, upperBoundry;

	[SerializeField] private Vector2 secondsBetweenSpawnsMinMax;
	float nextSpawnTime;

	[SerializeField] Vector2 spawnSizeMinMax;
	[SerializeField] float spawnAngleMax;

	void Update()
	{

		if (Time.timeSinceLevelLoad > nextSpawnTime)
		{
			float secondsBetweenSpawns = Mathf.Lerp(secondsBetweenSpawnsMinMax.y, secondsBetweenSpawnsMinMax.x, Difficulty.GetDifficulty());
			nextSpawnTime = Time.timeSinceLevelLoad + secondsBetweenSpawns;

			float spawnAngle = Random.Range(-spawnAngleMax, spawnAngleMax);
			float spawnSize = Random.Range(spawnSizeMinMax.x, spawnSizeMinMax.y);
			Vector2 spawnPosition = new Vector2(Random.Range(leftBorder.position.x, rightBorder.position.x), upperBoundry.position.y + spawnSize);
			GameObject newBlock = Instantiate(fallingBlockPrefab, spawnPosition, Quaternion.Euler(Vector3.forward * spawnAngle));
			newBlock.transform.localScale = Vector2.one * spawnSize;
		}

	}
}