using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameMessage : MonoBehaviour
{
	[SerializeField] private CustomScene[] customScenes;

	SpriteRenderer spriteRenderer;
	int chosenValue;

	bool canEnter;

	private void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
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

		int randomIndex = Random.Range(0, customScenes.Length);
		chosenValue = randomIndex;

		SceneManager.LoadScene(customScenes[randomIndex].buildIndex);

	}
}

[System.Serializable]
struct CustomScene
{
	public string name;
	public int buildIndex;
}
