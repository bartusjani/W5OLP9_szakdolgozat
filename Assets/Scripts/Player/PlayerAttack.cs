using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public Transform attackPoint;
    public Movement movement;
    public float attackRange = 0.5f;
    public int damage=5;
    public int strongDamage = 15;
    bool isCombo = false;
    public LayerMask enemyLayers;

    float attackTime = 0f;
    public int attackRate = 2;


    void Update()
    {

        if (Time.time >= attackTime)
        {
            if (Input.GetKeyDown(KeyCode.Q) && !movement.IsGrounded())
            {
                JumpAttack();
                attackTime = Time.time + 1f / attackRate;
            }
            else if (isCombo && Input.GetKeyDown(KeyCode.Q))
            {
                QuickAttackCombo();
                attackTime = Time.time + 1f / attackRate;
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                isCombo = true;
                QuickAttack();
                attackTime = Time.time + 1f / attackRate;
            }
            else if (Input.GetKeyDown(KeyCode.Y)) 
            {
                StrongAttack();
                attackTime = Time.time + 3f / attackRate;
            }
            else if (Input.GetKeyDown(KeyCode.X))
            {
                AreaAttack();
                attackTime = Time.time + 1f / attackRate;
            }

        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }


    void QuickAttack()
    {
        Debug.Log("quick attack");

        Collider2D[] hit = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hit)
        {
            enemy.GetComponent<EnemyHealth>().TakeDamage(damage);
        }
    }

    void StrongAttack()
    {
        Debug.Log("strong attack");
        Collider2D[] hit = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hit)
        {
            enemy.GetComponent<EnemyHealth>().TakeDamage(strongDamage);
        }
    }
    void QuickAttackCombo()
    {
        Debug.Log("quick combo attack");
        Collider2D[] hit = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hit)
        {
            enemy.GetComponent<EnemyHealth>().TakeDamage(damage+damage);
        }
    }
    void JumpAttack()
    {
        Debug.Log("jump attack");
        Collider2D[] hit = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hit)
        {
            enemy.GetComponent<EnemyHealth>().TakeDamage(damage);
        }
    }
    void AreaAttack()
    {
        Debug.Log("area attack");
        Collider2D[] hit = Physics2D.OverlapCircleAll(attackPoint.position, attackRange + 1.5f, enemyLayers);

        foreach (Collider2D enemy in hit)
        {
            enemy.GetComponent<EnemyHealth>().TakeDamage(damage);
        }
    }

}
