using System.Collections;
using UnityEngine;

public class MiniBossAttacks : MonoBehaviour
{
    EnemyHealth eh;
    public Transform attackPoint;
    public Transform areaAttackPoint;
    public Transform slashAttackPoint;
    public LayerMask playerLayer;

    float blockChance = 0.2f;
    public bool isBlocking = false;


    float strongCooldown = 2f;
    float areaCooldown = 3f;
    float blockCooldown = 2f;

    public float attackRange = 2f;
    public float areaAttackRange = 3f;

    int quickDamage = 10;
    int strongDamage = 40;
    int areaDamage = 20;
    int slashDamage = 50;

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

        //yield return new WaitForSeconds(1f);

        DealDamage(attackPoint, strongDamage);
        yield return new WaitForSeconds(strongCooldown);
    }

    IEnumerator PreformBlock()
    {
        isBlocking = true;
        Debug.Log("block");

        yield return new WaitForSeconds(5f);
        isBlocking = false;
    }
    IEnumerator PreformAreaAttack()
    {
        Debug.Log("AreaAttack");


        DealDamage(areaAttackPoint, areaDamage);

        yield return new WaitForSeconds(areaCooldown);
    }

    IEnumerator PreformForwardSlash()
    {
        Debug.Log("forward s");

        DealDamage(slashAttackPoint, slashDamage);

        yield return new WaitForSeconds(1f);
    }

    IEnumerator PreformQuickSlash()
    {
        Debug.Log("quick slash from boss");
        DealDamage(attackPoint, quickDamage);
        yield return new WaitForSeconds(0.2f);
    }

    void DealDamage(Transform attackPoint,int damage)
    {
        Collider2D player = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);

        if (player)
        {
            player.GetComponent<PlayerHealth>()?.TakeDamage(damage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        Gizmos.DrawWireSphere(areaAttackPoint.position, areaAttackRange);
        Gizmos.DrawWireSphere(slashAttackPoint.position, attackRange);
    }

}
