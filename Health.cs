using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private bool isDead = false;

    public HealthBar healthBar;

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        currentHealth = Mathf.Max(currentHealth - amount, 0);
        healthBar?.SetHealth(currentHealth);

        if (currentHealth <= 0) Die();
    }

    public void AddHealth(int amount)
    {
        if (isDead) return;

        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        healthBar?.SetHealth(currentHealth);
    }

    public void SetHealth(int health)
    {
        currentHealth = health;
        healthBar.SetHealth(health);
    }

    public int GetHealth()
    {
        return currentHealth;
    }

    void Die()
    {
        if (isDead) return;
        isDead = true;
    }

    public void ResetHealth(int maxHealth)
    {
        currentHealth = maxHealth;
        isDead = false;
    }
}
