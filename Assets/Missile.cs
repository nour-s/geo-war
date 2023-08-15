using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public float speed = 50f;

    public Vector3 explosionSpot;
    private Vector3 velocity = Vector3.zero;
    public float smoothTime = 0.5f;

    public GameObject bulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        explosionSpot = transform.right * 3;
    }

    // Update is called once per frame
    void Update()
    {

        // Move towards the target smoothly and explode
        transform.position = Vector3.SmoothDamp(transform.position, explosionSpot, ref velocity, smoothTime);

        // if the bomb is close enough to the target
        var distance = Vector3.Distance(transform.position, explosionSpot);
        if (distance <= 1)
        {
            Explode();
        }
    }

    private void Explode()
    {
        // Shoot eight bullets in each direction and then destry the missile
        for (int i = 0; i < 8; i++)
        {
            var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.transform.Rotate(0, 0, 45 * i);
        }

        Destroy(gameObject);
    }
}
