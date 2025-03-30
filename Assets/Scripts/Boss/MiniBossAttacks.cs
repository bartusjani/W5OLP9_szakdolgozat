using System.Collections;
using UnityEngine;

public class MiniBossAttacks : MonoBehaviour
{
    EnemyHealth eh;
    public Transform attackPoint;
    public Transform areaAttackPoint;
    public Transform slashAttackPoint;
    public LayerMask playerLayer;
    Rigidbody2D rb;
    Animator animator;
    Collider2D bossCollider;

    float blockChance = 0.2f;
    public bool isBlocking = false;


    float strongCooldown = 2f;
    float areaCooldown = 3f;
    float blockCooldown = 2f;

    public float attackRange = 2f;
    public float areaAttackRange = 5f;

    int quickDamage = 10;
    int strongDamage = 40;
    int areaDamage = 20;
    int slashDamage = 50;

    float nexAttackTime = 0f;

    bool isSlashAttack = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        eh=GetComponent<EnemyHealth>();
        bossCollider = GetComponent<Collider2D>();
    }


    public void ChoosePhase()
    {

        if(eh.Health < 250)
        {
            Phase2();
        }
        else
        {

            StartCoroutine(PreformForwardSlash());
            //Phase1();
        }
    }


    void Phase1()
    {
        if (Time.time >= nexAttackTime)
        {

            int attackRoll = Random.Range(0, 100);


            if (Random.Range(0f, 1f) <= blockChance)
            {
                StartCoroutine(PreformBlock());
                nexAttackTime += nexAttackTime + blockCooldown;
            }
            else
            {
                if (attackRoll < 70)
                {
                    StartCoroutine(PreformStrongSlash());
                    nexAttackTime += nexAttackTime + strongCooldown;
                }
                else if (attackRoll > 70)
                {
                    StartCoroutine(PreformAreaAttack());
                    nexAttackTime += nexAttackTime + areaCooldown;
                }
            }
        }
        else return;



    }
    void Phase2()
    {
        
        if (Time.time >= nexAttackTime)
        {

            int attackRoll = Random.Range(0, 100);


            if (Random.Range(0f, 1f) <= blockChance)
            {
                StartCoroutine(PreformBlock());
                nexAttackTime += nexAttackTime + blockCooldown;
            }
            else
            {
                if (attackRoll < 50)
                {
                    StartCoroutine(PreformForwardSlash());
                    nexAttackTime += nexAttackTime + 1f;
                }
                else
                {
                    StartCoroutine(PreformQuickSlash());
                    nexAttackTime += nexAttackTime + 0.2f;
                }
            }
        }
        else return;
    }

    IEnumerator PreformStrongSlash()
    {
        Debug.Log("strongSlash");

        animator.SetBool("StrongAttack", true);
        yield return new WaitForSeconds(1f);
        DealDamage(attackPoint, strongDamage);

        animator.SetBool("StrongAttack", false);

        yield return new WaitForSeconds(strongCooldown);
    }

    IEnumerator PreformBlock()
    {
        isBlocking = true;
        animator.SetBool("BlockStart", true);
        Debug.Log("block");

        yield return new WaitForSeconds(1f);
        animator.SetBool("BlockStart", false);
        isBlocking = false;
    }
    IEnumerator PreformAreaAttack()
    {
        Debug.Log("AreaAttack");

        animator.SetBool("AreaAttack", true);
        yield return new WaitForSeconds(1f);
        DealDamage(areaAttackPoint, areaDamage);
        animator.SetBool("AreaAttack", false);

        yield return new WaitForSeconds(areaCooldown);
    }

    IEnumerator PreformForwardSlash()
    {
        isSlashAttack = true;
        Debug.Log("forward s");


        Collider2D playerCollider = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Collider2D>();

        animator.SetBool("DashAttack", true);
        Physics2D.IgnoreCollision(bossCollider, playerCollider, true);

        float attackMoveSpeed = 10f;
        float attackDuration = 0.3f;
        float elapsedTime = 0f;

        Vector2 startPosition = rb.position;

        int direction = transform.localScale.x > 0 ? 1 : -1;
        Vector2 targetPosition = (Vector2)transform.position+new Vector2(5f * direction, 0);


        while (elapsedTime < attackDuration)
        {
            
            rb.linearVelocity = (targetPosition - startPosition).normalized * attackMoveSpeed;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rb.linearVelocity = Vector2.zero;
        transform.position = targetPosition;

        yield return new WaitForSeconds(1f);

        DealDamage(attackPoint, slashDamage);
        animator.SetBool("DashAttack", false);
        isSlashAttack = false;

        Physics2D.IgnoreCollision(bossCollider, playerCollider, false);

    }

    IEnumerator PreformQuickSlash()
    {
        Debug.Log("quick slash from boss");
        animator.SetBool("QuickAttack", true);

        yield return new WaitForSeconds(1f);

        animator.SetBool("QuickAttack", false);

        DealDamage(attackPoint, quickDamage);
    }

    void DealDamage(Transform attackPoint,int damage)
    {
        Collider2D player = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);

        if (player)
        {
            player.GetComponent<PlayerHealth>()?.TakeDamage(damage);
        }
    }

    public bool PlayerInDashArea()
    {
        if (!isSlashAttack) return true;

        Collider2D player = Physics2D.OverlapCircle(slashAttackPoint.position,attackRange, playerLayer);

        return player != null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        Gizmos.DrawWireSphere(areaAttackPoint.position, areaAttackRange);
        Gizmos.DrawWireSphere(slashAttackPoint.position, attackRange);
    }

}
