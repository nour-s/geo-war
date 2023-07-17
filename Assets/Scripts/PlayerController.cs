using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    readonly float moveSpeed = 7f;

    public GameObject bulletPrefab;

    public Transform firePoint;

    private bool isFiring = false;
    public float fireRate = 0.1f;
    public GameObject enemyPrefab;
    void Start()
    {
        // Start spawning enemies
        StartCoroutine(SpawnEnemies());
    }

    // Update is called once per frame
    void Update()
    {
        // var x = Input.GetAxis("Horizontal");


        Vector3 playerPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 distanceToMouse = Input.mousePosition - playerPos;

        float angle = Mathf.Atan2(distanceToMouse.y, distanceToMouse.x) * Mathf.Rad2Deg;
        var rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Rotate player towards the mouse
        transform.rotation = rotation;
        firePoint.rotation = rotation;

        var direction = Input.GetAxisRaw("Vertical");
        if (direction != 0)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            transform.position = Vector3.MoveTowards(transform.position, mousePosition, Time.deltaTime * moveSpeed * direction);
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
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation * Quaternion.Euler(0, 0, -90));
            yield return new WaitForSeconds(fireRate);
        }
    }

    private void SpawnEnemy()
    {
        // Calculate a random position within the game area
        Vector3 spawnPosition = new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), 0f);

        // Instantiate the enemy prefab at the spawn position
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }


    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            // Wait for a certain amount of time
            yield return new WaitForSeconds(Random.Range(1f, 3f));

            // Spawn an enemy
            SpawnEnemy();
        }
    }

}
