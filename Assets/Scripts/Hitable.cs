using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Hitable : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth = 100f;

    public float damage = 20f;

    public SpriteRenderer hitImpactSprite;

    public Slider healthBarSlider;

    public void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage()
    {
        FlashEffect();
        currentHealth -= damage;

        StartCoroutine(FlashEffect());

        if (healthBarSlider)
        {
            healthBarSlider.value = currentHealth / maxHealth;
        }

        if (currentHealth <= 0)
        {
            // Destroy the enemy
            Destroy(gameObject);
        }
    }

    private IEnumerator FlashEffect()
    {
        hitImpactSprite.enabled = true;
        yield return new WaitForSeconds(0.01f);
        hitImpactSprite.enabled = false;
    }
}
