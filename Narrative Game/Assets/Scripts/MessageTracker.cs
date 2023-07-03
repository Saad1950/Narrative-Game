using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MessageTracker : MonoBehaviour
{ 

	public static MessageTracker instance;
	[SerializeField] private PhoneController phoneController;
	[SerializeField] private Transform playerTransform;
	Vector2 savedPlayerPosition;

	[SerializeField] Rigidbody2D[] messageRbs;
	[SerializeField] List<Vector2> messagePositions = new List<Vector2>();


	// called first
	void OnEnable()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	// called second
	void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{

		if(scene.buildIndex == 0)
		{
			GetPhoneReference();
			LoadMessagesPos();
		}

	}

	// called when the game is terminated
	void OnDisable()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	private void Awake()
	{
		DontDestroyOnLoad(gameObject);

		if(instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
		}

	}

	private void Start()
	{
		GetPhoneReference();
	}

	private void LateUpdate()
	{
		if(Input.GetKeyDown(KeyCode.H))
		{
			SaveMessagesPos();
			SceneManager.LoadScene(1);
		}

		if(Input.GetKeyDown(KeyCode.G))
		{
			SceneManager.LoadScene(0);
		}

	}

	public void SaveMessagesPos()
	{
		//when this function is called, store the position of the messages when the scene was left

		for(int i = 0; i < messageRbs.Length; i++)
		{
			messagePositions.Add(messageRbs[i].transform.localPosition);
		}

		savedPlayerPosition = playerTransform.position;
	}

	void GetPhoneReference()
	{
		phoneController = FindAnyObjectByType<PhoneController>();
		messageRbs = phoneController.rbMessages;
		playerTransform = FindObjectOfType<PlayerMovement>().transform;
	}

	public void LoadMessagesPos()
	{
		//Load the positions
		for (int i = 0; i < messageRbs.Length; i++)
		{
			messageRbs[i].transform.localPosition = messagePositions[i];
		}

		playerTransform.position = savedPlayerPosition;

		//Remove the stored positions
		messagePositions.Clear();


	}


}
