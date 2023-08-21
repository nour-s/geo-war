using UnityEngine;

public class MissleDrop : MonoBehaviour
{
    public float floatSpeed = 1.0f; // Adjust the floating speed in the Inspector
    public float floatAmplitude = 0.5f; // Adjust the floating amplitude in the Inspector

    private Vector3 initialPosition;

    private void Awake()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        // Add a simple floating movement using sine wave
        Vector3 newPosition = initialPosition + Vector3.up * Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
        transform.position = newPosition;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.PlayerPickedMissle(this);
        }
    }
}
