using UnityEngine;

public class MiniBossMovement : MonoBehaviour
{
    Transform player;

    public float speed = 3f;
    public float stopDis = 10f;

    private Vector2 moveDir;

    private Rigidbody2D rb;

    EnemyHealth health;
    MiniBossAttacks mba;

    


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mba = GetComponent<MiniBossAttacks>();
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
            mba.ChoosePhase();
        }
    }

}
