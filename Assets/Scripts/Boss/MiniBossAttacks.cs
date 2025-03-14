using System.Collections;
using UnityEngine;

public class MiniBossAttacks : MonoBehaviour
{
    EnemyHealth eh;
    public Transform attackPoint;
    public Transform areaAttackPoint;
    public LayerMask playerLayer;

    float blockChance = 0.2f;
    public bool isBlocking = false;


    float strongCooldown = 2f;
    float areaCooldown = 3f;
    float blockCooldown = 2f;

    public float attackRange = 2f;
    public float areaAttackRange = 3f;

    int strongDamage = 40;
    int areaDamage = 20;

    float nexAttackTime = 0f;



    private void Start()
    {
        eh=GetComponent<EnemyHealth>();
    }


    public void ChoosePhase()
    {
        if(eh.Health < 250)
        {
            Phase2();
        }
        else
        {
            Phase1();
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

    }

    IEnumerator PreformStrongSlash()
    {
        Debug.Log("strongSlash");

        //yield return new WaitForSeconds(1f);

        Collider2D player = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);

        if (player)
        {
            player.GetComponent<PlayerHealth>()?.TakeDamage(strongDamage);
        }
        yield return new WaitForSeconds(strongCooldown);
    }

    IEnumerator PreformBlock()
    {
        isBlocking = true;
        Debug.Log("block");

        //yield return new WaitForSeconds(3f);

        yield return new WaitForSeconds(5f);
        isBlocking = false;
    }
    IEnumerator PreformAreaAttack()
    {
        Debug.Log("AreaAttack");

        //yield return new WaitForSeconds(6f);

        Collider2D player = Physics2D.OverlapCircle(areaAttackPoint.position, attackRange, playerLayer);

        if (player)
        {
            player.GetComponent<PlayerHealth>()?.TakeDamage(strongDamage);
        }

        yield return new WaitForSeconds(areaCooldown);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        Gizmos.DrawWireSphere(areaAttackPoint.position, areaAttackRange);
    }

}
