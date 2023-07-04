using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI timer;
	[SerializeField] private float totalTimeLeft;

	private void Start()
	{
		StartCoroutine(DecreaseTime());

	}

	private void Update()
	{
		totalTimeLeft = Mathf.Clamp(totalTimeLeft, 0f, totalTimeLeft);

		timer.text = totalTimeLeft.ToString();

		if(totalTimeLeft <= 0f)
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene(0);
		}
	}

	IEnumerator DecreaseTime()
	{
		while(true)
		{
			totalTimeLeft--;
			yield return new WaitForSeconds(1f);
		}
	}
}
