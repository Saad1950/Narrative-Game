using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MessagesManager : MonoBehaviour
{
	[SerializeField] private Transform messagesParent;
	[SerializeField] private GameObject messagePrefab;
	[SerializeField] private Transform messageYPosition;
	[SerializeField] private Vector2 positionToSizeYRatio = new Vector2(-1f, 2f);
	[Space]
	[SerializeField] private float spaceBetweenMessages = 2f;

	private const float preferredHeightInterval = 28.05f;

	[Header("Message Colours")]
	public MessageColour[] messageColours;
	[Space]
	[Header("Text Messages")]
	public TextMessage[] textMessages;

	private void Start()
	{
		SpawnTextMessages();
	}

	void SpawnTextMessages()
	{
		for(int i = 0; i <  textMessages.Length; i++)
		{
			Vector2 spawnPos = new Vector2(messageYPosition.localPosition.x, messageYPosition.localPosition.y + spaceBetweenMessages * i);
			GameObject spawnedTextMessage = Instantiate(messagePrefab, spawnPos, Quaternion.identity, messagesParent);
			


			TextMeshProUGUI text = spawnedTextMessage.GetComponentInChildren<TextMeshProUGUI>();
			text.text = textMessages[i].textMessage;

			SpriteRenderer spriteRenderer = spawnedTextMessage.GetComponentInChildren<SpriteRenderer>();
			spriteRenderer.color = GetColourFromEnum(textMessages[i].sender);
			Transform spriteTransfrom = spriteRenderer.transform;

			float preferredHeight = text.preferredHeight;
			int numberOfLines = Mathf.RoundToInt(preferredHeight / preferredHeightInterval);

			spriteTransfrom.localPosition = new Vector2(spriteTransfrom.localPosition.x, spriteTransfrom.localPosition.y + (positionToSizeYRatio.x * numberOfLines));
			spriteTransfrom.localScale = new Vector2(spriteTransfrom.localScale.x, spriteTransfrom.localScale.y + (positionToSizeYRatio.y * numberOfLines));

			float halfHeight = spriteRenderer.transform.localScale.y / 2f;
			spawnedTextMessage.transform.localPosition = new Vector2(spawnedTextMessage.transform.position.x, spawnedTextMessage.transform.position.y - halfHeight);
		}
	
	}

	Color GetColourFromEnum(Senders sender)
	{
		switch (sender)
		{
			case Senders.Dad:
				return messageColours[0].color;
			case Senders.Wife:
				return messageColours[1].color;
			case Senders.Son:
				return messageColours[2].color;
			default:
				return messageColours[0].color;
		}
	}

}
[System.Serializable]
public struct TextMessage
{
	public Senders sender;
	public string textMessage;
}
[System.Serializable]
public struct MessageColour
{
	public Senders sender;
	public Color color;
}


public enum Senders { Dad, Wife, Son }
