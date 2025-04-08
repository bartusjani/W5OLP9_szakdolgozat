using UnityEngine;

public class RoomTPToSewer : MonoBehaviour
{
    public GameObject interactText;
    bool isPlayerInTrigger = false;

    public Transform player;
    public Transform sewerRoom;
    GroundDoorTrigger gd;

    private void Start()
    {
        gd = GetComponent<GroundDoorTrigger>();
    }
    private void Update()
    {
        if (isPlayerInTrigger)
        {

            if (Input.GetKeyDown(KeyCode.E) && gd.groundDoorTrigger)
            {
                player.position = sewerRoom.position;
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
