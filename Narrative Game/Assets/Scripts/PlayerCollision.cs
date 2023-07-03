using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.CompareTag("Obstacle"))
		{
			gameObject.GetComponent<SpriteRenderer>().enabled = false;
			Invoke(nameof(LoadTextingScene), 3f);
		}
	}

	void LoadTextingScene()
	{
		SceneManager.LoadScene(0);
	}
	
}
