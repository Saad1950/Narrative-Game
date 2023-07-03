using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootingPosition;
    [SerializeField] private float minTimeBetweenShots = 0.5f;

    float nextTime;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }

    void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.timeSinceLevelLoad >= nextTime)
        {
            GameObject spawnedBullet = Instantiate(bulletPrefab, shootingPosition.position, Quaternion.identity);
            spawnedBullet.transform.rotation = Quaternion.Euler(0f, 0f, -90f);
            nextTime = Time.timeSinceLevelLoad + minTimeBetweenShots;
        }
    }
}
