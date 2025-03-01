using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 2f;
    public int damage = 10;

    private Vector2 dir;

    public PlayerHealth ph;

    public static int blockedBullets = 0;

    public void SetDir(Vector2 direction)
    {
        dir = direction.normalized;
    }
    void Update()
    {
        transform.position += (Vector3)dir * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ph= collision.gameObject.GetComponent<PlayerHealth>();
            ph.TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag=="Shield")
        {
            blockedBullets++;
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
