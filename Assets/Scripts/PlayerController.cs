using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    readonly float moveSpeed = 7f;

    public GameObject bulletPrefab;

    public Transform firePoint;

    private bool isFiring = false;
    public float fireRate = 0.1f;

    // Update is called once per frame
    void Update()
    {
        // var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxisRaw("Vertical") > 0 ? 1 : 0;

        Vector3 playerPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 direction = Input.mousePosition - playerPos;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        var rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Rotate player towards the mouse
        transform.rotation = rotation;
        firePoint.rotation = rotation;

        if (y != 0)
        {

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            transform.position = Vector3.MoveTowards(transform.position, mousePosition, Time.deltaTime * moveSpeed);
        }

        // Check if the player is firing
        if (Input.GetMouseButtonDown(0))
        {
            isFiring = true;
            StartCoroutine(FireBullets());
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isFiring = false;
        }

    }

    private IEnumerator FireBullets()
    {
        while (isFiring)
        {
            // Create a bullet
            var bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation * Quaternion.Euler(0, 0, -90));
            yield return new WaitForSeconds(fireRate);
        }
    }
}
