using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlock : MonoBehaviour
{
    public Vector2 speedMinMax;
    float borderDestruction;

	private void Start()
	{
        borderDestruction = -Camera.main.orthographicSize - transform.localScale.x;
	}

	// Update is called once per frame
	void Update()
    {
        float speed = Mathf.Lerp(speedMinMax.x, speedMinMax.y, Difficulty.GetDifficulty());
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if(transform.position.y < borderDestruction)
		{
            Destroy(gameObject);
		}

    }
}
