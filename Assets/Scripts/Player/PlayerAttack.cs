using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public int damage=5;
    public LayerMask enemyLayers;

    float attackTime = 0f;
    public int attackRate = 2;


    void Update()
    {

        if (Time.time >= attackTime)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Attack();
                attackTime = Time.time + 1f / attackRate;
            }
        }
    }

    void Attack()
    {
        Collider2D[] hit= Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hit)
        {
            enemy.GetComponent<EnemyHealth>().TakeDamage(damage);
        }

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
