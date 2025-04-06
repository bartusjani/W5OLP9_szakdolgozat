using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Trigger : MonoBehaviour
{

    public GameObject door;
    public Transform doorWaypointTarget;
    public GameObject popUp;
    public GameObject text;
    public WayPointUI waypoint;


    void Start()
    {
        text.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            waypoint.SetTarget(doorWaypointTarget.transform);
            text.SetActive(true);
            door.SetActive(false);
            popUp.SetActive(false);

        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            text.SetActive(false);

        }
    }
}
