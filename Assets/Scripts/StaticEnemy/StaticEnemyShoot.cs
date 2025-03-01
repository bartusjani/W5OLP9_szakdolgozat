using UnityEngine;
using UnityEngine.Rendering;

public class EnemyShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firepoint;
    public float fireRate = 1f;
    private float fireTimer;

    private bool canShoot;

    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    private void Update()
    {
        if (player == null) return;

        fireTimer += Time.deltaTime;

        if (fireTimer >= fireRate)
        {
            Shoot();
            fireTimer = 0;
        }

        if (Bullet.blockedBullets == 3)
        {
            canShoot = false;
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firepoint.position, Quaternion.identity);
        Vector2 dir = (player.position - firepoint.position).normalized;
        bullet.GetComponent<Bullet>().SetDir(dir);
    }
}
