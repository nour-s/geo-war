using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 4f;
    public float rotationSpeed = 200f;
    public float attackRange = 2f;
    public float attackCooldown = 1f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 0.7f;

    private Transform playerTransform;
    private bool canAttack = true;

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
        // Move towards the player
        transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, moveSpeed * Time.deltaTime);

        // Rotate towards the player
        Vector3 direction = playerTransform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
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
                var bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                bullet.GetComponent<Bullet>().Shooter = tag;

                // Cooldown before the next attack
                StartCoroutine(ResetAttackCooldown());
            }
        }
    }

    private IEnumerator ResetAttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}
