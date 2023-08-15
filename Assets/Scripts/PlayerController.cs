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

    public GameObject missilePrefab;

    public GameObject character;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>() ?? throw new MissingComponentException("Missing audio source component");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 distanceToMouse = Input.mousePosition - playerPos;

        float angle = Mathf.Atan2(distanceToMouse.y, distanceToMouse.x) * Mathf.Rad2Deg;
        var rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Rotate player towards the mouse
        character.transform.rotation = rotation;
        firePoint.rotation = rotation;

        var direction = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0).normalized;

        // Move in the direction of keyboard arrow
        transform.position += Time.deltaTime * moveSpeed * direction;

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

        if (Input.GetMouseButtonDown(1))
        {
            // Shoot a missile
            var instance = Instantiate(missilePrefab, firePoint.position, firePoint.rotation);
            var missile = instance.GetComponent<Missile>();
            missile.shootMissile = true;
        }

    }

    private IEnumerator FireBullets()
    {
        while (isFiring)
        {
            // Create a bullet
            var bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation * Quaternion.Euler(0, 0, -90));
            bullet.GetComponent<Bullet>().Shooter = tag;
            audioSource.Play();
            yield return new WaitForSeconds(fireRate);
        }
    }
}
