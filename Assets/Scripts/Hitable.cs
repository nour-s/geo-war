using System.Collections;
using UnityEngine;

public class Hitable : MonoBehaviour
{
    public float health = 100f;

    public float damage = 20f;

    public SpriteRenderer spriteRenderer;

    public void TakeDamage()
    {
        FlashEffect();
        health -= damage;

        StartCoroutine(FlashEffect());

        if (health <= 0)
        {
            // Destroy the enemy
            Destroy(gameObject);
        }
    }

    private IEnumerator FlashEffect()
    {
        spriteRenderer.enabled = true;
        yield return new WaitForSeconds(0.01f);
        spriteRenderer.enabled = false;
    }
}
