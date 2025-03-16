using UnityEditor.Tilemaps;
using UnityEngine;

public class MiniBossMovement : MonoBehaviour
{
    Transform player;

    public float speed = 3f;
    public float stopDis = 3f;

    private Vector2 moveDir;
    private bool facingRight = true;

    private Rigidbody2D rb;

    EnemyHealth health;
    MiniBossAttacks mba;

    


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mba = GetComponent<MiniBossAttacks>();
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }
    private void Update()
    {
        FlipTowardsPlayer();
        if (player != null)
        {

            TargetingPlayer();

        }
    }
    private void FixedUpdate()
    {
        rb.linearVelocity = moveDir * speed;
    }

    void TargetingPlayer()
    {
        if (player == null) return;
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
    void FlipTowardsPlayer()
    {
        if(player.position.x<transform.position.x && facingRight)
        {
            Flip();
        }
        else if(player.position.x > transform.position.x && !facingRight)
        {
            Flip();
        }
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 newS = transform.localScale;
        newS.x *= -1;
        transform.localScale = newS;
    }

}
