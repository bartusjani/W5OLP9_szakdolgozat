using UnityEngine;

public class BossDoorTriggers : MonoBehaviour
{
    public GameObject bossHpBar;
    public GameObject boss;
    public GameObject trigger;
    public GameObject libraryTP;

    public Transform player;
    public Transform room;

    public bool isPlayerInTrigger = false;

    private void Update()
    {
        if (isPlayerInTrigger)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                player.position = room.position;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && EnemyHealth.isBossDead)
        {
            isPlayerInTrigger = true;
            libraryTP.SetActive(true);
        }
        else if (collision.gameObject.tag == "Player")
        {
            bossHpBar.SetActive(true);
            boss.SetActive(true);
            trigger.SetActive(false);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
        }
    }
}
