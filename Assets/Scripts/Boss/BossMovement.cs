using UnityEngine;

public class BossMovement : MonoBehaviour
{
    Transform player;
    public float speed = 3f;
    public float stopDis = 10f;
    private Vector2 moveDir;
    private Rigidbody2D rb;
    EnemyHealth health;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        TargetingPlayer();
    }
    private void FixedUpdate()
    {
        rb.linearVelocity = moveDir * speed;
    }

    void TargetingPlayer()
    {
        float distToPlayer = Vector2.Distance(transform.position, player.position);

        if(distToPlayer > stopDis)
        {
            moveDir= (player.position -transform.position).normalized;
        }
        else
        {
            moveDir = Vector2.zero;
            ChooseAttack();
        }
    }
    void ChooseAttack()
    {
        if (health.Health >= 200)//phase1
        {
            
        }
        else//phase2
        {

        }
    }

}
