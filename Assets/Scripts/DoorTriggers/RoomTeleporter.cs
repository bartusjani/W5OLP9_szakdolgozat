using System.Collections;
using UnityEngine;

public class RoomTeleporter : MonoBehaviour
{
    public GameObject interactText;
    bool isPlayerInTrigger = false;

    public Transform player;
    public Transform tutorialRoom;
    public ScreenFading fader;
    bool isFading = false;

    private void Start()
    {
        fader = GetComponent<ScreenFading>();
    }

    private void Update()
    {
        if (isPlayerInTrigger)
        {

            if (Input.GetKeyDown(KeyCode.E) && !isFading)
            {
                StartCoroutine(TeleportWithFade());
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

    IEnumerator TeleportWithFade()
    {
        isFading = true;
        interactText.SetActive(false);

        Movement playerMovement = player.GetComponent<Movement>();
        if (playerMovement != null) playerMovement.enabled = false;


        fader.FadeToBlack();


        yield return new WaitUntil(() => fader.IsFadingComplete());

        player.position = tutorialRoom.position;

        fader.FadeToClear();

        yield return new WaitForSeconds(1f);
        isFading = false;
        if (isPlayerInTrigger) interactText.SetActive(true);
        if (playerMovement != null) playerMovement.enabled = true;
    }
}
