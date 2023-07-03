using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerScript : MonoBehaviour
{
    [SerializeField] bool DestroyOnLifetime;
    [SerializeField] private float lifeTime = 10f;

    // Start is called before the first frame update
    void Start()
    {
        if(DestroyOnLifetime)
		    Destroy(gameObject, lifeTime);
	}


}
