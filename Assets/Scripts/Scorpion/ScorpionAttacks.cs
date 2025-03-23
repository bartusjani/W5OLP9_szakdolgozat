using System.Collections;
using UnityEngine;

public class ScorpionAttacks : MonoBehaviour
{

    public Transform attackPoint;
    public LayerMask playerLayer;

    Animator animator;

    public float attackRange = 1f;

    public int quickSlashDamage = 10;
    public float quickAttackCooldown = 0.5f;

    public int StrongSlashDamage = 20;
    public float strongSlashCooldown = 1f;

    public int forwardSlashDamage = 10;
    public float forwardSlashCooldown = 1.5f;

    public bool isBlocking = false;
    public float blockCooldown=2f;

    private float nextAttack = 0f;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Time.time >= nextAttack && PlayerInRange())
        {
            ChooseAttack();
        }
    }


    bool PlayerInRange()
    {
        return Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer) != null;
    }

    Vector2 PlayerPos()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        return player ? player.transform.position : Vector2.zero;
    }

    void ChooseAttack()
    {
        float distanceFromPlayer = Vector2.Distance(transform.position, PlayerPos());


        if (distanceFromPlayer < 1f)
        {
            StartCoroutine(Block());
        }
        if (distanceFromPlayer < 1.5f)
        {
            StartCoroutine(QuickSlash());
        }
        else if (distanceFromPlayer > 3f)
        {
            StartCoroutine(ForwardSlash());
        }
        else
        {
            StartCoroutine(StrongSlash());
        }
    }

    IEnumerator QuickSlash()
    {
        
        Debug.Log("quick");
        animator.SetBool("isQuickAttack", true);

        yield return new WaitForSeconds(1f);
        animator.SetBool("isQuickAttack", false);
        nextAttack = Time.time + quickAttackCooldown;
        DealDamage(quickSlashDamage);

        yield return new WaitForSeconds(nextAttack);
    }
    IEnumerator ForwardSlash()
    {
        //Debug.Log("forward");

        yield return new WaitForSeconds(1f);
        nextAttack = Time.time + forwardSlashCooldown;
        DealDamage(forwardSlashDamage);

        yield return new WaitForSeconds(nextAttack);
    }

    IEnumerator StrongSlash()
    {
        animator.SetBool("isStrongAttack",true);
        Debug.Log("strong");
        yield return new WaitForSeconds(1f);
        nextAttack = Time.time + strongSlashCooldown;
        DealDamage(StrongSlashDamage);
        yield return new WaitForSeconds(nextAttack);
    }

    IEnumerator Block()
    {
        isBlocking = true;

        animator.SetBool("isBlocking", isBlocking);

        yield return new WaitForSeconds(1f);
        isBlocking = false;
        animator.SetBool("isBlocking", isBlocking);
        yield return new WaitForSeconds(blockCooldown);
    }

    void DealDamage(int damage)
    {
        Collider2D player = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);

        if (player)
        {
            player.GetComponent<PlayerHealth>()?.TakeDamage(damage);
        }
    }
}
