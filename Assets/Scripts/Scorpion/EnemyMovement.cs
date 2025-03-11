using Unity.Mathematics;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float moveSpeed;
    public int patrolDestination;
    public Transform groundCheck;
    Animator animator;
    Rigidbody2D rb;

    public Transform player;
    public bool isChasing=false;
    public float chaseDistance;


    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        RaycastHit2D groundInfo = Physics2D.Raycast(groundCheck.position, Vector2.down, 2f);

        animator.SetBool("isWalking", true);
        if (isChasing)
        {
            if (groundInfo.collider == false)
            {
                isChasing = false;
            }
            else
            {
                if (transform.position.x > player.position.x)
                {
                    transform.localScale = new Vector3((float)-0.06303474, (float)0.05184435, (float)1.0141);
                    transform.position += Vector3.left * moveSpeed * Time.deltaTime;
                }
                else if (transform.position.x < player.position.x)
                {
                    transform.localScale = new Vector3((float)0.06303474, (float)0.05184435, (float)1.0141);
                    transform.position += Vector3.right * moveSpeed * Time.deltaTime;
                }
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
