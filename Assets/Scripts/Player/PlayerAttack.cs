using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public int damage=20;
    public LayerMask enemyLayers;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Attack();
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
