using System.Collections;
using UnityEngine;

public class HumanAttacks : MonoBehaviour
{
    public LayerMask playerLayer;

    public Transform attackPoint;
    public Transform forwardAttackPoint;

    Animator animator;

    public float attackRange = 1.5f;
    public float blockChance = 0.5f;

    public int quickDamage = 15;
    public float quickCooldown = 0.5f;

    public int strongDamage = 25;
    public float strongCooldown = 1f;

    public int forwardDamage = 15;
    public float forwardCooldown = 1.5f;

    public bool isBlocking = false;
    public float blockCooldown = 2f;

    private float nextAttackTime = 0f;

    private void Start()
    {
        animator = GetComponent<Animator>();
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
    public void ChooseAttack()
    {
        if (Time.time < nextAttackTime) return;

        int attackRoll = Random.Range(0, 100);

        if (Random.Range(0f, 1f) <= blockChance)
        {
            StartCoroutine(PreformBlock());
            nextAttackTime += Time.time + blockCooldown;
        }
        else if(attackRoll < 30)
        {
            StartCoroutine(ForwardSlash());
            nextAttackTime += Time.time + forwardCooldown;
        }
        else if (attackRoll < 50)
        {
            StartCoroutine(QuickSlash());
            nextAttackTime += Time.time + quickCooldown;
        }
        else
        {
            StartCoroutine(StrongSlash());
            nextAttackTime = Time.time + strongCooldown;
        }
    }

    IEnumerator QuickSlash()
    {
        Debug.Log("QuickAttack");


        DealDamage(attackPoint, quickDamage);

        yield return new WaitForSeconds(quickCooldown);
    }
    IEnumerator StrongSlash()
    {
        Debug.Log("strongAttack");


        DealDamage(attackPoint, strongDamage);

        yield return new WaitForSeconds(strongCooldown);
    }

    IEnumerator ForwardSlash()
    {
        Debug.Log("forwardAttack");


        DealDamage(forwardAttackPoint, forwardDamage);

        yield return new WaitForSeconds(forwardCooldown);
    }
    IEnumerator PreformBlock()
    {
        isBlocking = true;
        Debug.Log("block");

        yield return new WaitForSeconds(5f);
        isBlocking = false;
    }

    void DealDamage(Transform attackPoint, int damage)
    {
        Collider2D player = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);

        if (player)
        {
            player.GetComponent<PlayerHealth>()?.TakeDamage(damage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        Gizmos.DrawWireSphere(forwardAttackPoint.position, attackRange);
    }
}
