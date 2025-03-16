using UnityEngine;

public class BossDoorTriggers : MonoBehaviour
{
    public GameObject groundDoor;
    public GameObject bossHpBar;
    public GameObject boss;
    public GameObject trigger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            groundDoor.SetActive(true);
            bossHpBar.SetActive(true);
            boss.SetActive(true);
            trigger.SetActive(false);
        }
    }
}
