using UnityEngine;

public class Bullet : MonoBehaviour
{
    // A bullet is an object that move fast forward the moment it gets initialized
    public float speed = 10f;
    public float lifeTime = 1f;
    public Vector3 direction = Vector3.zero;


    // Start is called before the first frame update
    void Start()
    {
        // Destroy the bullet after a certain amount of time
        Destroy(gameObject, lifeTime);
    }


    // Update is called once per frame
    void Update()
    {
        // Move the bullet in the direction using transform
        transform.position += transform.up * speed * Time.deltaTime;
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
