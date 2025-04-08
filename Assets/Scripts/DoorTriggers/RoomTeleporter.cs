using UnityEngine;

public class RoomTeleporter : MonoBehaviour
{
    public GameObject interactText;
    bool isPlayerInTrigger = false;

    public Transform player;
    public Transform tutorialRoom;

    private void Update()
    {
        if (isPlayerInTrigger)
        {

            if (Input.GetKeyDown(KeyCode.E))
            {
                player.position = tutorialRoom.position;
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
