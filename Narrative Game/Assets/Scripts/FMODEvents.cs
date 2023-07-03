using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
	public static FMODEvents instance { get; private set; }

	[field: Header("Music")]
	[field: SerializeField] public EventReference cakeStackingOST { get; private set; }


	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(gameObject);
			return;
		}
	}
}


