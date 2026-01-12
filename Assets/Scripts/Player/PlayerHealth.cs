using UnityEngine;

public class PlayerManager : MonoBehaviour, IShopPurchase
{
    public float maxHealth = 100f;
    public float currentHealth;
    private DropsManager dropsManager;
    private void Start()
    {
        currentHealth = maxHealth;
        dropsManager = Object.FindFirstObjectByType<DropsManager>();
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
        // just destroy the player
        Destroy(gameObject);
    }

    public void BoughtItem(ShopItem.ItemType itemType)
    {
        Debug.Log("Player bought item: " + itemType);
    }

    public bool CanBuyItem(int coinAmount)
    {
        if (dropsManager != null)
        {
            return dropsManager.SpendCoins(coinAmount);
        }
        return false;
    }
}
    