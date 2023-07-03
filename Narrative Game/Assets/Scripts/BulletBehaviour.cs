using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] private float bulletForce = 10f;
	[SerializeField] private float removalDelay = 5f;


	Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
		Destroy(gameObject, removalDelay);
		rb = GetComponent<Rigidbody2D>();
        ImpulseShoot();
    }

    void ImpulseShoot()
    {
        rb.AddForce(Vector2.right * bulletForce, ForceMode2D.Impulse);
    }
}
