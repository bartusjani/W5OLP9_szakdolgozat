using UnityEditor;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int health;
    public HealthBar healthbar;

    PlayerRespawn pr;
    bool died;

    void Start()
    {
        died = false;
        health = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
        pr = GameObject.Find("Player").GetComponent<PlayerRespawn>();
    }

    private void Update()
    {
        if (died)
        {
            health = maxHealth;
            healthbar.SetMaxHealth(maxHealth);
        }
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        healthbar.setHealth(health);
        if (health <= 0) Die();
    }
    public void Die()
    {
        died = true;
        pr.Respawn();
    }
}
