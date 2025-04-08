using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Trigger : MonoBehaviour
{

    public bool isDoorOpen = false;
    public GameObject interactText;
    bool isPlayerInTrigger = false;

    public PopUpBubble prefab;
    PopUpBubble activeBubble;
    public string message;
    public Sprite image;

    public Transform doorWaypointTarget;
    public WayPointUI waypoint;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isPlayerInTrigger)
        {
            
            interactText.SetActive(false);

            if (activeBubble == null)
            {
                Transform parent = GameObject.Find("PopUps").transform;

                activeBubble = Instantiate(prefab, parent);
                activeBubble.SetText(message);
                activeBubble.SetSprite(image);
            }

            waypoint.SetTarget(doorWaypointTarget.transform);
            isDoorOpen = true;
            //GetComponent<Collider2D>().enabled = false;
        }
        if (!isPlayerInTrigger)
        {
            if (activeBubble != null)
            {
                Destroy(activeBubble.gameObject);
                activeBubble = null;
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
