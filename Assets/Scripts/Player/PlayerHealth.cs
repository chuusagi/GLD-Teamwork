using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        Debug.Log("Player Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth, maxHealth); // cap at max health

        Debug.Log("Player healed! Health: " + currentHealth);
    }

    private void Die()
    {
        Debug.Log("Player Died!");
        // Add death logic here (respawn, game over screen, etc.)
        // For now, just destroy the player
        Destroy(gameObject);
    }
}