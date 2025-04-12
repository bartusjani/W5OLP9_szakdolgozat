using System.Collections;
using UnityEngine;

public class RoomTeleporterFromTutorialRoom: MonoBehaviour
{

    public Trigger tr;
    public GameObject interactText;
    bool isPlayerInTrigger = false;

    public Transform player;
    public Transform combatTutorialRoom;
    ScreenFading fader;

    private void Start()
    {
        tr = FindFirstObjectByType<Trigger>();
        fader = GetComponent<ScreenFading>();
    }
    private void Update()
    {
        if (tr.isDoorOpen && isPlayerInTrigger)
        {
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(TeleportWithFade());
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

    IEnumerator TeleportWithFade()
    {
        
        Movement playerMovement = player.GetComponent<Movement>();
        if (playerMovement != null) playerMovement.enabled = false;

        
        fader.FadeToBlack();

        
        yield return new WaitUntil(() => fader.IsFadingComplete());

        player.position = combatTutorialRoom.position;

        fader.FadeToClear();

        if (playerMovement != null) playerMovement.enabled = true;
    }
}
