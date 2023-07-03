using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CakeBehaviour : MonoBehaviour
{
	private float timeInTrigger = 0f;

	private void OnTriggerStay2D(Collider2D collision)
	{
		if(collision.CompareTag("WinBoundry"))
			timeInTrigger += Time.deltaTime;
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("WinBoundry"))
			timeInTrigger = 0f;
	}

	private void Update()
	{
		CheckWinCondition();
	}

	void CheckWinCondition()
	{
		if(timeInTrigger >= 2f)
		{
			Invoke(nameof(LoadTextingScene), 3f);
		}
	}

	void LoadTextingScene()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene(0);
	}
}
