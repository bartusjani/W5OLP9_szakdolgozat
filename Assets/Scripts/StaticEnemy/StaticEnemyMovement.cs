using System.Collections;
using UnityEngine;

public class StaticEnemyPlaceChange : MonoBehaviour
{
    
    public Transform[] waypoints;
    Animator animator;
    private int currWayPointIndex = 0;
    bool isMoving = false;
    bool isDying = false;

    public EnemyHealth enemyHealth;
    public EnemyHpBar enemyHpBar;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isDying && enemyHealth.Health <= 0)
        {
            StopAllCoroutines();
            StartCoroutine(Died());
            return;
        }

        if (enemyHealth.Health <= 20 && currWayPointIndex < waypoints.Length && !isMoving)
        {
            
            StartCoroutine(MovePlace());
            Debug.Log("currWayp:" + currWayPointIndex + "waypoint length: " + waypoints.Length);
        }
        else if(currWayPointIndex == waypoints.Length)
        {
            isDying = true;
        }
    }

    IEnumerator Died()
    {
        animator.SetBool("isDead", true);

        yield return new WaitForSeconds(1.3f);
        
    }

    IEnumerator MovePlace()
    {
        if (isDying) yield break;

        isMoving = true;
        animator.SetBool("Move", true);

        yield return new WaitForSeconds(1.3f);
        transform.position = waypoints[currWayPointIndex].position;
        
        if(currWayPointIndex < waypoints.Length)
        {
            currWayPointIndex ++;
        }

        animator.SetBool("Move", false);
        isMoving = false;
        enemyHealth.Health = 50;
        enemyHpBar.setHealth(enemyHealth.Health); 

        animator.Play("static_enemy_spawn");
    }
}
