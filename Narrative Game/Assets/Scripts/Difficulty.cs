using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Difficulty
{
	static float timeDifficulty = 60f;

	public static float GetDifficulty()
	{
		return Mathf.Clamp01(Time.timeSinceLevelLoad / timeDifficulty);
	}
}
