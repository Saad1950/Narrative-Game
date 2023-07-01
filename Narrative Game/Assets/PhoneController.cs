using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneController : MonoBehaviour
{
    [SerializeField] private float scrollingSpeed;
    [SerializeField] GameObject messageHolder;

    Rigidbody2D[] rbMessages;
    private const float speedMultiplier = 100f;
    Vector2 input;

	private void Start()
	{
        //Gets the rigidbodies of the messages and turns off their gravity and freezes rotation among other things
        rbMessages = messageHolder.GetComponentsInChildren<Rigidbody2D>();
        foreach(Rigidbody2D rb  in rbMessages)
        {
            rb.gravityScale = 0f;
            rb.freezeRotation = true;
            rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        }
	}

	private void Update()
    {
        GetInput();
    }

	private void FixedUpdate()
	{
        ScrollDown();
	}

    private void GetInput()
    {
        //Gets the input based on the up and down arrows

        float scrollValue = 0f;

        if (Input.GetKey(KeyCode.DownArrow))
        {
            scrollValue = -1f;
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            scrollValue = 1f;
        }
        else
        {
            scrollValue = 0f;
        }

        input = new Vector2(0f, scrollValue);

    }
    private void ScrollDown()
    {
        //Applies an upward or downwards force based on the input
        foreach(Rigidbody2D rb in rbMessages)
        {
            rb.velocity = input * scrollingSpeed * Time.deltaTime * speedMultiplier;
        }
    }
}
