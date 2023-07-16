using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // A bullet is an object that move fast forward the moment it gets initialized
    public float speed = 1f;
    public float lifeTime = 1f;
    public Vector3 direction = Vector3.zero;
    private float angle;


    // Start is called before the first frame update
    void Start()
    {
        // Destroy the bullet after a certain amount of time
        Destroy(gameObject, lifeTime);
    }

    /// Shoot is a  function that takes a direction and then moves the bullet in that direction
    public void Shoot(Vector3 direction)
    {
        //rotate the bullet towards the direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // set the direction of the bullet
        this.direction = direction;
        transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
        Debug.Log("Direction is " + direction);
    }

    // Update is called once per frame
    void Update()
    {
        if (direction == Vector3.zero)
        {
            Debug.Log("Direction is zero");
            return;
        }

        // Move the bullet in the direction using transform
        // transform.Translate(direction.normalized * speed * Time.deltaTime);
        transform.position += direction.normalized * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the bullet hits an enemy
        if (collision.CompareTag("Enemy"))
        {
            // Destroy the enemy
            Destroy(collision.gameObject);
        }
    }
}
