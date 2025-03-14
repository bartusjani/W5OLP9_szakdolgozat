using System.Collections;
using UnityEngine;

public class MiniBossAttacks : MonoBehaviour
{
    EnemyHealth eh;
    float blockChance = 0.2f;

    float strongCooldown = 2f;
    float areaCooldown = 3f;



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
        int attackRoll = Random.Range(0, 100);


        if (Random.Range(0f, 1f) <= blockChance)
        {
            StartCoroutine(PreformBlock());
        }
        else
        {
            if(attackRoll < 70)
            {
                StartCoroutine(PreformStrongSlash());
            }
            else if(attackRoll > 70)
            {
                StartCoroutine(PreformAreaAttack());
            }
        }



    }
    void Phase2()
    {

    }

    IEnumerator PreformStrongSlash()
    {
        Debug.Log("strongSlash");

        yield return new WaitForSeconds(1f);


        yield return new WaitForSeconds(strongCooldown);
    }

    IEnumerator PreformBlock()
    {
        Debug.Log("block");

        yield return new WaitForSeconds(3f);

        yield return new WaitForSeconds(5f);
    }
    IEnumerator PreformAreaAttack()
    {
        Debug.Log("AreaAttack");

        yield return new WaitForSeconds(6f);

        yield return new WaitForSeconds(areaCooldown);
    }
}
