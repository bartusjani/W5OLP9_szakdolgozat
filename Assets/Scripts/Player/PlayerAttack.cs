using System.Collections;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;


public class PlayerAttack : MonoBehaviour
{

    public Transform attackPoint;
    public Transform jumpAttackPoint;
    public Transform areaAttackPoint;
    public Movement movement;
    public GameObject shield;
    Animator animator;

    public int damage=5;
    public int strongDamage = 15;
    public int areaDamage = 10;


    bool isCombo = false;
    public bool isBlocking = false;
    public float blockDur = 1f;
    public float blockCooldown = 3f;
    private bool canBlock = true;

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
            if(Input.GetKeyDown(KeyCode.Tab) && canBlock &&movement.IsGrounded())
            {
                StartCoroutine(Block());
            }
            else if (Input.GetKeyDown(KeyCode.Q) && !movement.IsGrounded())
            {
                StartCoroutine(JumpAttack());
                attackTime = Time.time + 1f / attackRate;
            }
            else if (isCombo && Input.GetKeyDown(KeyCode.Q) && movement.IsGrounded())
            {
                StartCoroutine(QuickAttackCombo());
                attackTime = Time.time + 1f / attackRate;
            }
            else if (Input.GetKeyDown(KeyCode.Q) && movement.IsGrounded())
            {
                isCombo = true;
                StartCoroutine(QuickAttack());
                attackTime = Time.time + 1f / attackRate;
            }
            else if (Input.GetKeyDown(KeyCode.Y) && movement.IsGrounded()) 
            {
                StartCoroutine(StrongAttack());
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


    IEnumerator Block()
    {
        canBlock = false;
        isBlocking = true;
        shield.SetActive(true);

        animator.SetBool("isBlocking", isBlocking);

        yield return new WaitForSeconds(blockDur);

        shield.SetActive(false);
        isBlocking = false;
        animator.SetBool("isBlocking", isBlocking);

        yield return new WaitForSeconds(blockCooldown);
        canBlock = true;
    }

    IEnumerator QuickAttack()
    {
        Debug.Log("quick attack");

        animator.SetBool("isAttacking",true);
        yield return new WaitForSeconds(1f);

        animator.SetBool("isAttacking", false);

        Collider2D[] hit = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hit)
        {
            enemy.GetComponent<EnemyHealth>().TakeDamage(damage);
        }
        yield return new WaitForSeconds(1f);
    }

    IEnumerator StrongAttack()
    {
        Debug.Log("strong attack");

        animator.SetBool("isStrongAt", true);

        yield return new WaitForSeconds(1f);

        animator.SetBool("isStrongAt", false);

        Collider2D[] hit = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hit)
        {
            enemy.GetComponent<EnemyHealth>().TakeDamage(strongDamage);
        }

        yield return new WaitForSeconds(1f);
    }
    IEnumerator QuickAttackCombo()
    {
        Debug.Log("quick combo attack");

        animator.SetBool("isComboAt", true);

        yield return new WaitForSeconds(1f);

        animator.SetBool("isComboAt", false);

        Collider2D[] hit = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hit)
        {
            enemy.GetComponent<EnemyHealth>().TakeDamage(damage+damage);
        }

        yield return new WaitForSeconds(1f);
    }
    IEnumerator JumpAttack()
    {
        Debug.Log("jump attack");

        animator.SetBool("isAttacking", true);

        yield return new WaitForSeconds(1f);

        animator.SetBool("isAttacking", false);

        Collider2D[] hit = Physics2D.OverlapCircleAll(jumpAttackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hit)
        {
            enemy.GetComponent<EnemyHealth>().TakeDamage(damage);
        }

        yield return new WaitForSeconds(1f);
    }
    IEnumerator AreaAttack()
    {
        animator.SetBool("isAreaAttack", true);
        Debug.Log("area attack");

        float attackDur =1.01f;


        yield return new WaitForSeconds(attackDur);
        Collider2D[] hit = Physics2D.OverlapCircleAll(areaAttackPoint.position, areaAttackRange , enemyLayers);

        foreach (Collider2D enemy in hit)
        {
            enemy.GetComponent<EnemyHealth>().TakeDamage(areaDamage);
        }


        animator.SetBool("isAreaAttack", false);

        yield return new WaitForSeconds(attackTime);

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isBlocking && collision.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
        }
    }

}
