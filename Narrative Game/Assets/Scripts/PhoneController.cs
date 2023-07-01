using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class PhoneController : MonoBehaviour
{
    [SerializeField] private float scrollingSpeed;
    [SerializeField] GameObject messageHolder;

    Rigidbody2D[] rbMessages;
    private const float speedMultiplier = 100f;
    Vector2 input;

    //Lerp variables
    [SerializeField] private AnimationCurve curve;
    float current = 0f, target = 1f;
    float lerpSpeed = 1f;


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

        if(Input.GetKey(KeyCode.DownArrow))
        {
            scrollValue = LerpValue(scrollValue, false);
        }
        else
        {
            scrollValue = LerpValue(scrollValue, true);
        }

        input = new Vector2(0f, scrollValue) ;

    }

    float LerpValue(float value, bool down)
    {
        if(down == false)
		{
			current = Mathf.MoveTowards(current, target, lerpSpeed * Time.deltaTime);
			value = Mathf.Lerp(value, -1f, curve.Evaluate(current));

		}
        else
        {
			current = Mathf.MoveTowards(current, target, lerpSpeed * Time.deltaTime);
			value = Mathf.Lerp(value, 0f, curve.Evaluate(current));
		}

        return value;
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
