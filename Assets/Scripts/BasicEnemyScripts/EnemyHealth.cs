using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public int maxHealth = 20;
    private int health;
    public EnemyHpBar hpBar;
    public GameObject bossHpBar;
    public GameObject trapDoor;
    public static event Action OnAnyEnemyDeath;

    public bool isBoss = false;
    public bool isScorpion = false;

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
        if (isBoss)
        {
            MiniBossAttacks mba = GetComponent<MiniBossAttacks>();
            if (mba.isBlocking)
            {
                Debug.Log("Blocked attack");
                return;
            }
        }
        else if (isScorpion)
        {
            ScorpionAttacks sa = GetComponent<ScorpionAttacks>();
            if (sa.isBlocking)
            {
                Debug.Log("Blocked attack");
                return;
            }
        }

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
        if (isBoss)
        {
            bossHpBar.SetActive(false);
            trapDoor.SetActive(false);
        }
    }
    
}
