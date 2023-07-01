using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlocksPlayerMovement : MonoBehaviour
{
	[SerializeField] private Transform leftBorder, rightBorder;
	float leftBorderBoundry, rightBorderBoundry;

	[SerializeField] private float speed = 7f;

	// Start is called before the first frame update
	void Start()
	{
		float halfPlayerWidth = transform.localScale.x / 2f;
		leftBorderBoundry = leftBorder.position.x - halfPlayerWidth;
		rightBorderBoundry = rightBorder.position.x + halfPlayerWidth;
	}

	// Update is called once per frame
	void Update()
	{
		#region Movement Calculation
		float inputXKeyboard = Input.GetAxisRaw("Horizontal");
		float velocity = inputXKeyboard * speed;
		transform.Translate(Vector2.right * velocity * Time.deltaTime);

		if (transform.position.x > rightBorderBoundry)
		{
			transform.position = new Vector2(leftBorderBoundry, transform.position.y);
		}
		if (transform.position.x < leftBorderBoundry)
		{
			transform.position = new Vector2(rightBorderBoundry, transform.position.y);
		}
		#endregion

	}

	bool IsNegative(float value)
	{
		if (value >= 0)
		{
			return false;
		}
		else if (value <= 0)
		{
			return true;
		}
		return true;
	}

}
