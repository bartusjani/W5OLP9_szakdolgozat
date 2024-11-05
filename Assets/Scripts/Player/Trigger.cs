using UnityEngine;

public class Trigger : MonoBehaviour
{

    public GameObject door;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            door.SetActive(false);
        }
    }
}
