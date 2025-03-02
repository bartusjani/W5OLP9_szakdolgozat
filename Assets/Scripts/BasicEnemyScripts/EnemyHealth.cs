using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public int maxHealth = 20;
    private int health;
    public EnemyHpBar hpBar;
    public static event Action OnAnyEnemyDeath;

    public int Health
    {
        get { return health; }
        set { health = value; }
    }

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

    public void Die()
    {
        OnAnyEnemyDeath?.Invoke();
        Destroy(gameObject);
    }
    
}
