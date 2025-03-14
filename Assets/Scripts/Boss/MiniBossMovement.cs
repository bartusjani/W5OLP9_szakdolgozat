using UnityEngine;

public class MiniBossMovement : MonoBehaviour
{
    Transform player;

    public float speed = 3f;
    public float stopDis = 3f;

    private Vector2 moveDir;

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

}
