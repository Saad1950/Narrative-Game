using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
	public static bool RandomTrueOrFalse()
	{
		int randomValue = Random.Range(0, 2);

		if (randomValue == 0)
		{
			return false;
		}
		else if(randomValue == 1) 
		{
			return true;
		}
		else
		{
			return false;
		}


	}
}
