using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int health;
    public HealthBar healthbar;

    void Start()
    {
        health = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        healthbar.setHealth(health);
        if (health <= 0) Destroy(gameObject);
    }
}
