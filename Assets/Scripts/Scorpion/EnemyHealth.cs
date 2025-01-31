using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public int maxHealth = 20;
    private int health;
    public ScorpionHpBar hpBar;
    public static event Action OnAnyEnemyDeath;
    void Start()
    {
        health = maxHealth;
        hpBar.SetMaxHealth(maxHealth);
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        hpBar.setHealth(health);
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        OnAnyEnemyDeath?.Invoke();
        Destroy(gameObject);
    }
    
}
