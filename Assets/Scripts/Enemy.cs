using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 40f;
    public float rotationSpeed = 200f;
    public float attackRange = 20f;
    public float attackCooldown = 1f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 3f;

    public GameObject character;

    private Transform playerTransform;
    private bool canAttack = true;

    public AudioSource shootSound;

    private Vector3 velocity = Vector3.zero;
    public float smoothTime = 0.5f;
    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        if (playerTransform == null)
        {
            throw new Exception("Player not found");
        }

        StartCoroutine(AttackPlayer());
    }

    private void Update()
    {
        // Calculate the direction from the enemy to the player
        Vector3 directionToPlayer = playerTransform.position - transform.position;

        // Calculate the target position that maintains the desired distance
        Vector3 targetPosition = playerTransform.position - directionToPlayer.normalized * 3;


        // Move towards the player
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        // Rotate towards the player
        Vector3 direction = playerTransform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        character.transform.rotation = Quaternion.RotateTowards(character.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private IEnumerator AttackPlayer()
    {
        while (true)
        {
            yield return new WaitForSeconds(attackCooldown);

            var distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
            if (distanceToPlayer <= attackRange && canAttack)
            {
                // Shoot a bullet
                var bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                var bullet = bulletGO.GetComponent<Bullet>();
                bullet.Shooter = tag;
                bullet.Color = Color.green;
                shootSound.Play();

                // Cooldown before the next attack
                StartCoroutine(ResetAttackCooldown());
            }
        }
    }

    private IEnumerator ResetAttackCooldown()
    {
        canAttack = false;
        var randomCooldown = UnityEngine.Random.Range(0.1f, attackCooldown);
        yield return new WaitForSeconds(randomCooldown);
        canAttack = true;
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }
}
