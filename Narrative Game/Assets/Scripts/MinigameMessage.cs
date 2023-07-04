using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameMessage : MonoBehaviour
{
	MessageTracker messageTracker;
	SpriteRenderer spriteRenderer;

	private static int chosenValue;


	CustomScene[] scenes;

	bool canEnter;

	private void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		messageTracker = FindObjectOfType<MessageTracker>();
		scenes = messageTracker.scenes;
		spriteRenderer.color = Color.red;

		
	}

	private void Update()
	{
		if(canEnter && Input.GetKeyDown(KeyCode.Mouse1))
			EnterMinigame();
			
		
	}


	private void OnTriggerStay2D(Collider2D collision)
	{
		if(collision.CompareTag("Player"))
			canEnter = true;
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
			canEnter = false;

	}

	void EnterMinigame()
	{
		MessageTracker.instance.SaveMessagesPos();
		SceneManager.LoadScene(1);


	}
}


