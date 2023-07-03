using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalagaPlayerMovement : MonoBehaviour
{
	[SerializeField] private Transform bottomBorder, topBorder;
	float bottomBorderBoundry, topBorderBoundry;

	[SerializeField] private float movementSpeed = 7f;

	// Start is called before the first frame update
	void Start()
	{
		float halfPlayerWidth = transform.localScale.y / 2f;
		bottomBorderBoundry = bottomBorder.position.y - halfPlayerWidth;
		topBorderBoundry = topBorder.position.y + halfPlayerWidth;
	}

	// Update is called once per frame
	void Update()
	{
		#region Movement Calculation
		float inputYKeyboard = Input.GetAxisRaw("Vertical");
		float velocity = inputYKeyboard * movementSpeed;
		transform.Translate(Vector2.up * velocity * Time.deltaTime);

		if (transform.position.y > topBorderBoundry)
		{
			transform.position = new Vector2(transform.position.x, bottomBorderBoundry);
		}
		if (transform.position.y < bottomBorderBoundry)
		{
			transform.position = new Vector2(transform.position.x, topBorderBoundry);
		}
		#endregion

	}
}
