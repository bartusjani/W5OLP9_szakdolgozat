using UnityEngine;

public class ScorpionAttacks : MonoBehaviour
{

    public Transform attackPoint;
    public LayerMask playerLayer;

    public float attackRange = 1f;

    public int quickSlashDamage = 10;
    public float quickAttackCooldown = 0.5f;

    public int StrongSlashDamage = 20;
    public float strongSlashCooldown = 1f;

    public int forwardSlashDamage = 10;
    public float forwardSlashCooldown = 1.5f;

    private float nextAttack = 0f;

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

        if (distanceFromPlayer < 1.5f)
        {
            QuickSlash();
        }
        else if (distanceFromPlayer > 3f)
        {
            ForwardSlash();
        }
        else
        {
            StrongSlash();
        }
    }

    void QuickSlash()
    {
        Debug.Log("quick");
        nextAttack = Time.time + quickAttackCooldown;
        DealDamage(quickSlashDamage);
    }
    void ForwardSlash()
    {
        Debug.Log("forward");
        nextAttack = Time.time + forwardSlashCooldown;
        DealDamage(forwardSlashDamage);
    }

    void StrongSlash()
    {
        Debug.Log("strong");
        nextAttack = Time.time + strongSlashCooldown;
        DealDamage(StrongSlashDamage);
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
