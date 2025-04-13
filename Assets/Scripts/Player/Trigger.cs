using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Trigger : MonoBehaviour
{

    public bool isDoorOpen = false;
    public GameObject interactText;
    bool isPlayerInTrigger = false;
    bool wasSpeaking = false;

    public PopUpBubble prefab;
    public string message;
    public Sprite image;

    public string objMessage;

    public SpeechBubble speechPrefab;
    public string speechMessage;

    private PopUpBubble activeBubble;
    private PopUpBubble objActiveBubble;
    private SpeechBubble speechActiveBubble;


    public Transform doorWaypointTarget;
    public WayPointUI waypoint;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isPlayerInTrigger)
        {
            
            interactText.SetActive(false);
            if (!wasSpeaking)
            {
                StartCoroutine(SetPopUp(message,objMessage, speechMessage));
            }

           waypoint.SetTarget(doorWaypointTarget.transform);
           isDoorOpen = true;

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
            if (activeBubble != null)
            {
                Destroy(activeBubble.gameObject);
                activeBubble = null;
            }
            if (objActiveBubble != null)
            {
                Destroy(objActiveBubble.gameObject);
                objActiveBubble = null;
            }
            if (speechActiveBubble != null)
            {
                Destroy(speechActiveBubble.gameObject);
                speechActiveBubble = null;
            }
            GetComponent<Collider2D>().enabled = false;
        }
    }


    IEnumerator SetPopUp(string message,string objMessage)
    {
        if (activeBubble == null)
        {
            Transform parent = GameObject.Find("PopUps").transform;

            activeBubble = Instantiate(prefab, parent);
            activeBubble.SetText(message);

            yield return new WaitForSeconds(1f);
            SetObjcective(objMessage);

        }
    }

    IEnumerator SetPopUp(string message,string objMessage, string speechMessage)
    {
        if (activeBubble == null)
        {
            Transform parent = GameObject.Find("PopUps").transform;

            activeBubble = Instantiate(prefab, parent);
            activeBubble.SetText(message);

            yield return new WaitForSeconds(1f);
            SetObjcective(objMessage);

            yield return new WaitForSeconds(0.5f);
            SetSpeech(speechMessage);
            wasSpeaking = true;
        }
    }

    void SetObjcective(string message)
    {
        if (objActiveBubble == null)
        {
            Transform parent = GameObject.Find("PopUps").transform;

            objActiveBubble = Instantiate(prefab, parent);
            objActiveBubble.SetText(message);

        }
    }
    void SetSpeech(string message)
    {
        if (speechActiveBubble == null && !wasSpeaking)
        {
            Transform parent = GameObject.Find("PlayerSpeechBubbles").transform;

            speechActiveBubble = Instantiate(speechPrefab, parent);
            speechActiveBubble.SetText(message);
            //wasSpeaking = true;
        }
    }
}
