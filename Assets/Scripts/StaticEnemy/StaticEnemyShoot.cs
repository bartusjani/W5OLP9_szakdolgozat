using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyShoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firepoint;

    Animator fireballAnimator;

    public float fireRate = 1f;
    private float fireTimer;

    private bool canShoot = true;
    private Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

    }

    private void Update()
    {
        if (player == null|| !canShoot) return;

        fireTimer += Time.deltaTime;

        if (fireTimer >= fireRate)
        {
            Shoot();
            fireTimer = 0;
        }

        if (Bullet.blockedBullets >= 3)
        {
            
            StartCoroutine(ShootingPaused());
        }
    }

    private void Shoot()
    {
        if (canShoot)
        {

            GameObject bullet = Instantiate(bulletPrefab, firepoint.position, Quaternion.identity);

            fireballAnimator = bullet.GetComponent<Animator>();

            fireballAnimator.Play("shooting_fireball");

            Vector2 dir = (player.position - firepoint.position).normalized;
            bullet.GetComponent<Bullet>().SetDir(dir);
        }
    }

    private IEnumerator ShootingPaused()
    {
        canShoot = false;
        Bullet.blockedBullets = 0;
        yield return new WaitForSeconds(3f);
        canShoot = true;
    }
}
