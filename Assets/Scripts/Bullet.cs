using UnityEngine;

public class Bullet : MonoBehaviour
{
    // A bullet is an object that move fast forward the moment it gets initialized
    public float speed = 10f;

    public string Shooter { get; set; }

    public Color Color = Color.red;

    // Start is called before the first frame update
    void Start()
    {
        // Set the color of the bullet
        GetComponent<SpriteRenderer>().color = Color;
    }


    // Update is called once per frame
    void Update()
    {
        // Move the bullet in the direction using transform
        transform.position += transform.up * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Shooter == collision.tag || collision.tag == tag)
        {
            return;
        }

        var hitable = collision.GetComponent<Hitable>();
        hitable?.TakeDamage();

        Destroy(gameObject);
    }
}
