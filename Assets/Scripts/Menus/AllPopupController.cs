using System.Collections;
using UnityEngine;

public class AllPopupController : MonoBehaviour
{
    public PopUpBubble popPrefab;
    public string popMessage;


    public ObjectiveBubble objPrefab;
    public string objMessage;


    public SpeechBubble speechPrefab;
    public string speechMessage;


    private PopUpBubble activeBubble;
    private ObjectiveBubble objActiveBubble;
    private SpeechBubble speechActiveBubble;

    public GameObject platformerHelp;
    int helpCounter = 0;

    public Trigger tr;
    public bool isTutorialRoom = false;
    bool isPlayerInTrigger = false;
    private bool wasSpeaking = false;


    private void Update()
    {
        if (isTutorialRoom)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                platformerHelp.SetActive(false);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isTutorialRoom)
        {
            if (collision.CompareTag("Player") && !tr.isDoorOpen)
            {

                isPlayerInTrigger = true;
                if (!wasSpeaking)
                {

                    StartCoroutine(SetPopUp(popMessage,objMessage, speechMessage));
                }
                else
                {
                    if (activeBubble == null && objActiveBubble == null)
                    {
                        StartCoroutine(SetPopUp(popMessage,objMessage));
                    }
                }
            }
        }
        if (collision.CompareTag("Player"))
        {

            isPlayerInTrigger = true;
            if (!wasSpeaking)
            {

                StartCoroutine(SetPopUp(popMessage, objMessage, speechMessage));
            }
            else
            {
                if (activeBubble == null && objActiveBubble == null)
                {
                    StartCoroutine(SetPopUp(popMessage, objMessage));
                }
            }
        }

        else
        {
            isPlayerInTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
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
            if (isTutorialRoom &&helpCounter==0)
            {
                platformerHelp.SetActive(true);
                helpCounter = 1;
            }
        }
    }
    IEnumerator SetPopUp(string message ,string objMessage)
    {

        if (activeBubble == null)
        {
            Transform parent = GameObject.Find("PopUps").transform;

            activeBubble = Instantiate(popPrefab, parent);
            activeBubble.SetText(popMessage);


            yield return new WaitForSeconds(1f);
            SetObjective(objMessage);


        }
    }

    IEnumerator SetPopUp(string message, string objMessage, string speechMessage)
    {
        
        if (activeBubble == null)
        {
            Transform parent = GameObject.Find("PopUps").transform;

            activeBubble = Instantiate(popPrefab, parent);
            activeBubble.SetText(message);

            yield return new WaitForSeconds(1f);
            SetObjective(objMessage);

            yield return new WaitForSeconds(0.5f);
            SetSpeech(speechMessage);
            wasSpeaking = true;
        }
    }

    void SetObjective(string message)
    {
        if (objActiveBubble == null)
        {
            Transform parent = GameObject.Find("ObjectiveBubbles").transform;

            objActiveBubble = Instantiate(objPrefab, parent);
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
            wasSpeaking = true;
        }
    }
}
