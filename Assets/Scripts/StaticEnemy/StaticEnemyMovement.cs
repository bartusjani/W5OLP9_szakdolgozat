using UnityEngine;

public class StaticEnemyPlaceChange : MonoBehaviour
{
    
    public Transform[] waypoints;
    Animator animator;
    private int currWayPointIndex = -1;

    public EnemyHealth enemyHealth;
    public EnemyHpBar enemyHpBar;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (enemyHealth.Health <= 20 && currWayPointIndex != waypoints.Length-1)
        {
            
            MovePlace();
            Debug.Log("currWayp:" + currWayPointIndex + "waypoint length: " + waypoints.Length);
        }
        else if (enemyHealth.Health <= 0)
        {
            animator.SetBool("isDead", true);
            enemyHealth.Die();
        }
    }

    public void MovePlace()
    {
        animator.SetBool("Move", true);
        enemyHealth.Health = 50;
        enemyHpBar.setHealth(enemyHealth.Health); 
        currWayPointIndex++;
        animator.SetBool("Move", false);
        transform.position = waypoints[currWayPointIndex].position;
        
    }
}
