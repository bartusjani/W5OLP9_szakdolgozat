using Unity.Mathematics;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float moveSpeed;
    public int patrolDestination;

    public Transform player;
    public bool isChasing=false;
    public float chaseDistance;


    void Update()
    {

        if (isChasing)
        {
            if (transform.position.x > player.position.x)
            {
                transform.localScale = new Vector3((float)-0.06303474, (float)0.05184435, (float)1.0141);
                transform.position += Vector3.left*moveSpeed*Time.deltaTime;
            }
            else if(transform.position.x < player.position.x)
            {
                transform.localScale = new Vector3((float)0.06303474, (float)0.05184435, (float)1.0141);
                transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            }
        }
        else
        {

            if (Vector2.Distance(transform.position, player.position) < chaseDistance) isChasing = true;

            switch (patrolDestination)
            {
                case 0:
                    transform.position = Vector2.MoveTowards(transform.position,patrolPoints[0].position,moveSpeed*Time.deltaTime);
                    if (Vector2.Distance(transform.position, patrolPoints[0].position) < .2f)
                    {
                        transform.localScale = new Vector3((float)-0.06303474, (float)0.05184435, (float)1.0141);
                        patrolDestination = 1;
                    }
                    break;
                case 1:
                    transform.position = Vector2.MoveTowards(transform.position, patrolPoints[1].position, moveSpeed * Time.deltaTime);
                    if (Vector2.Distance(transform.position, patrolPoints[1].position) < .2f)
                    {
                        transform.localScale = new Vector3((float)0.06303474, (float)0.05184435, (float)1.0141);
                        patrolDestination = 0;
                    }
                    break;

                default:
                    break;
            }
        }

       
    }
}
