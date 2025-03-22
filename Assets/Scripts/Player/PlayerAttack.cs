using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public Transform attackPoint;
    public Transform jumpAttackPoint;
    public Transform areaAttackPoint;
    public Movement movement;
    Animator animator;

    public int damage=5;
    public int strongDamage = 15;
    public int areaDamage = 10;


    bool isCombo = false;
    public LayerMask enemyLayers;

    public float attackRange = 0.5f;
    public float areaAttackRange = 3f;
    float attackTime = 0f;
    public int attackRate = 2;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {

        if (Time.time >= attackTime)
        {
            if (Input.GetKeyDown(KeyCode.Q) && !movement.IsGrounded())
            {
                JumpAttack();
                attackTime = Time.time + 1f / attackRate;
            }
            else if (isCombo && Input.GetKeyDown(KeyCode.Q) && movement.IsGrounded())
            {
                QuickAttackCombo();
                attackTime = Time.time + 1f / attackRate;
            }
            else if (Input.GetKeyDown(KeyCode.Q) && movement.IsGrounded())
            {
                isCombo = true;
                QuickAttack();
                attackTime = Time.time + 1f / attackRate;
            }
            else if (Input.GetKeyDown(KeyCode.Y) && movement.IsGrounded()) 
            {
                StrongAttack();
                attackTime = Time.time + 3f / attackRate;
            }
            else if (Input.GetKeyDown(KeyCode.X) && movement.IsGrounded())
            {
                StartCoroutine(AreaAttack());
                attackTime = Time.time + 1f / attackRate;
                
            }

        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        Gizmos.DrawWireSphere(jumpAttackPoint.position, attackRange);
        Gizmos.DrawWireSphere(areaAttackPoint.position, areaAttackRange);
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
        Collider2D[] hit = Physics2D.OverlapCircleAll(jumpAttackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hit)
        {
            enemy.GetComponent<EnemyHealth>().TakeDamage(damage);
        }
    }
    IEnumerator AreaAttack()
    {
        animator.SetBool("isAreaAttack", true);
        Debug.Log("area attack");

        float attackDur =0.8f;
        yield return new WaitForSeconds(attackDur);
        Collider2D[] hit = Physics2D.OverlapCircleAll(areaAttackPoint.position, areaAttackRange , enemyLayers);

        foreach (Collider2D enemy in hit)
        {
            enemy.GetComponent<EnemyHealth>().TakeDamage(areaDamage);
        }


        animator.SetBool("isAreaAttack", false);

        yield return new WaitForSeconds(attackTime);

        
    }

}
