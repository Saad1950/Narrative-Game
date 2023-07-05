using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
	public int numOfHearts = 1;

	private void Update()
	{
		HandleHealth();
	}
	void HandleHealth()
	{
		numOfHearts = Mathf.Clamp(numOfHearts, 0, 100);

		if (numOfHearts == 0)
		{
			Destroy(gameObject);
		}
	}

}
