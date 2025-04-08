using UnityEngine;

public class RoomTP : MonoBehaviour
{
    public GameObject interactText;
    bool isPlayerInTrigger = false;

    public Transform player;
    public Transform room;

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
        if (collision.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
            interactText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
            interactText.SetActive(false);
        }
    }
}
