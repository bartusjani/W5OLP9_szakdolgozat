using UnityEngine;

public class StaticEnemyPlaceChange : MonoBehaviour
{
    public Transform[] waypoints;
    private int currWayPointIndex = -1;

    public EnemyHealth enemyHealth;
    public EnemyHpBar enemyHpBar;

    private void Update()
    {
        if (enemyHealth.Health <= 20 && currWayPointIndex != waypoints.Length-1)
        {
            MovePlace();
            Debug.Log("currWayp:" + currWayPointIndex + "waypoint length: " + waypoints.Length);
        }
        else if (enemyHealth.Health <= 0)
        {
            enemyHealth.Die();
        }
    }

    public void MovePlace()
    {
        enemyHealth.Health = 50;
        enemyHpBar.setHealth(enemyHealth.Health); 
        currWayPointIndex++;
        transform.position = waypoints[currWayPointIndex].position;
    }
}
