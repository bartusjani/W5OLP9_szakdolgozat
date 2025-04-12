using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class RoomTP : MonoBehaviour
{
    public GameObject interactText;

    bool isPlayerInTrigger = false;
    public bool isResearchRoom = false;
    public bool isBossRoom = false;

    public Transform player;
    Movement playerMovement;
    Rigidbody2D playerRb;
    public Transform room;

    ScreenFading fader;
    public bool isFading = false;
    public Image faderImage;

    public float teleportCooldown = 1.5f;
    private float lastTeleportTime;


    public GameObject WarRoomScorpion1;
    public GameObject WarRoomScorpion2;
    GroundDoorTrigger gd;
    public Trigger tr;

    private void Start()
    {
        fader = GetComponent<ScreenFading>();
        lastTeleportTime = -teleportCooldown;
        gd = GetComponent<GroundDoorTrigger>();
        tr = FindFirstObjectByType<Trigger>();
    }
    private void Update()
    {
        if (isPlayerInTrigger)
        {
            if (isResearchRoom)
            {
                WarRoomScorpion1.SetActive(true);
                WarRoomScorpion2.SetActive(true);
            }
            if (isBossRoom)
            {
                if (Input.GetKeyDown(KeyCode.E) && EnemyHealth.isBossDead)
                {
                    StartCoroutine(TeleportWithFade());
                }
            }
            else if (Input.GetKeyDown(KeyCode.E) && tr.isDoorOpen)
            {
                StartCoroutine(TeleportWithFade());
            }
            //else if (Input.GetKeyDown(KeyCode.E) && gd.groundDoorTrigger && CanTeleport())
            //{
            //    StartCoroutine(TeleportWithFade());
            //}
            else if (Input.GetKeyDown(KeyCode.E) && CanTeleport())
            {
                StartCoroutine(TeleportWithFade());
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (isBossRoom)
            {
                if (EnemyHealth.isBossDead)
                {
                    isPlayerInTrigger = true;
                    playerMovement = player.GetComponent<Movement>();
                    playerRb = player.GetComponent<Rigidbody2D>();
                    interactText.SetActive(true);
                    faderImage.gameObject.SetActive(true);
                }
            }
            else
            {
                isPlayerInTrigger = true;
                playerMovement = player.GetComponent<Movement>();
                playerRb = player.GetComponent<Rigidbody2D>();
                interactText.SetActive(true);
                faderImage.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
            isFading = false;
            interactText.SetActive(false);
            faderImage.gameObject.SetActive(false);
        }
    }

    private bool CanTeleport()
    {
        return !isFading && (Time.time > lastTeleportTime + teleportCooldown);
    }

    IEnumerator TeleportWithFade()
    {
        if (isFading) yield break;

        isFading = true;
        lastTeleportTime = Time.time;

        interactText.SetActive(false);
        FreezePlayer(true);

        fader.FadeToBlack();


        yield return new WaitUntil(() => fader.IsFadingComplete());

        player.position = room.position;

        fader.FadeToClear();
        yield return new WaitUntil(() => fader.IsFadingComplete());

        if (isPlayerInTrigger) interactText.SetActive(true);
        FreezePlayer(false);
        yield return new WaitForSeconds(1f);
        isFading = false;

    }

    void FreezePlayer(bool freeze)
    {
        if (player != null && freeze)
        {
            playerMovement.enabled = false;
            playerRb.linearVelocity = Vector2.zero;
            playerRb.bodyType = RigidbodyType2D.Kinematic;
        }
        else
        {
            playerMovement.enabled = true;
            playerRb.bodyType = RigidbodyType2D.Dynamic;
        }
    }
}
