using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LinkPlayerCollision : MonoBehaviour
{
	[SerializeField] private int playerHealth = 3;
	[SerializeField] private GameObject heartContainer;
	[SerializeField] private Texture2D brokenHeartTexture;

	bool isDefending;
	RawImage[] heartImages;

	private void Start()
	{
		heartImages = heartContainer.GetComponentsInChildren<RawImage>();
	}

	void Defend()
	{
		if (Input.GetKey(KeyCode.Mouse1))
		{
			isDefending = true;
			print("isDefending");
		}
		else if(Input.GetKeyUp(KeyCode.Mouse1))
		{
			isDefending = false;
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.collider.CompareTag("Enemy") && !isDefending)
		{
			playerHealth--;
			heartImages[playerHealth].texture = brokenHeartTexture;
			
		}
	}

	private void Update()
	{
		Defend();

		if(playerHealth == 0)
		{
			Destroy(gameObject);
		}
	}
}
