using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CakeBehaviour : MonoBehaviour
{
	private float timeInTrigger = 0f;
	bool isWinning;

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.CompareTag("WinBoundry"))
			isWinning = true;
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("WinBoundry"))
			isWinning = false;
	}

	private void Update()
	{
		CheckWinCondition();

	}

	void CheckWinCondition()
	{
		if (isWinning)
		{
			timeInTrigger += Time.deltaTime;
		}
		else
		{
			timeInTrigger = 0f;

		}

		if (timeInTrigger >= 2f)
		{
			Invoke(nameof(LoadTextingScene), 3f);
		}
	}

	void LoadTextingScene()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene(0);
	}
}
