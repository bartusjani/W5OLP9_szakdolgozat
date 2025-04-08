using UnityEngine;

public class RoomTeleporterFromTutorialRoom: MonoBehaviour
{

    public Trigger tr;
    public GameObject interactText;
    bool isPlayerInTrigger = false;

    public Transform player;
    public Transform combatTutorialRoom;

    private void Start()
    {
        tr = FindFirstObjectByType<Trigger>();
    }
    private void Update()
    {
        if (tr.isDoorOpen && isPlayerInTrigger)
        {
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                player.position = combatTutorialRoom.position;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactText.SetActive(true);
            isPlayerInTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactText.SetActive(false);
            isPlayerInTrigger = false;
        }
    }
}
