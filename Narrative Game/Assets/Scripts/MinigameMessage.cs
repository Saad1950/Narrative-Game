using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameMessage : MonoBehaviour
{
	[SerializeField] private CustomScene[] customScenes;

	SpriteRenderer spriteRenderer;
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

	void EnterMinigame()
	{
		MessageTracker.instance.SaveMessagesPos();
		SceneManager.LoadScene(customScenes[Random.Range(0, customScenes.Length)].buildIndex);

	}
}

[System.Serializable]
struct CustomScene
{
	public string name;
	public int buildIndex;
}
