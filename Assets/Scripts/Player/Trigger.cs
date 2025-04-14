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

    public PopUpBubble popUpPrefab;
    string popUpMessage;

    public ObjectiveBubble objPrefab;
    string objMessage;

    public SpeechBubble speechPrefab;
    string speechMessage;

    private PopUpBubble activeBubble;
    private ObjectiveBubble objActiveBubble;
    private SpeechBubble speechActiveBubble;

    private TextAsset popUpText;
    private TextAsset speechText;
    private TextAsset objectiveText;

    public Transform doorWaypointTarget;
    public WayPointUI waypoint;
    bool onlypopUp = false;

    ItemAdder itemAdder;

    public InventoryPage invPage;
    public Sprite itemImage;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isPlayerInTrigger)
        {
            itemAdder=gameObject.GetComponent<ItemAdder>();
            itemAdder.AddItemToInv(invPage,itemImage,"aaaaa","bbbbbb");
            interactText.SetActive(false);
            if (!wasSpeaking)
            {
                ChooseTexts(2);
                StartCoroutine(SetPopUp(objMessage, speechMessage));
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
            ChooseTexts(2);
            StartCoroutine(SetPopUp(popUpMessage));
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
            StopAllCoroutines();
            GetComponent<Collider2D>().enabled = false;
        }
    }

    IEnumerator SetPopUp(string message)
    {
        if (activeBubble == null)
        {
            Transform parent = GameObject.Find("PopUps").transform;

            activeBubble = Instantiate(popUpPrefab, parent);
            activeBubble.SetText(message);
            yield return new WaitForSeconds(2f);
            if (activeBubble != null) 
            {
                Destroy(activeBubble.gameObject);
                activeBubble = null;

            }
        }
    }
    IEnumerator SetPopUp(string message,string objMessage)
    {
        if (activeBubble == null || objActiveBubble==null)
        {
            if (activeBubble != null)
            {
                Destroy(activeBubble.gameObject);
            }
            Transform parent = GameObject.Find("PopUps").transform;

            activeBubble = Instantiate(popUpPrefab, parent);
            activeBubble.SetText(message);

            yield return new WaitForSeconds(0.2f);
            SetObjective(objMessage);

        }
    }

    IEnumerator SetPopUp(string message,string objMessage, string speechMessage)
    {
        if (activeBubble == null)
        {
            Transform parent = GameObject.Find("PopUps").transform;

            activeBubble = Instantiate(popUpPrefab, parent);
            activeBubble.SetText(message);

            yield return new WaitForSeconds(0.2f);
            SetObjective(objMessage);

            yield return new WaitForSeconds(0.1f);
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
            //wasSpeaking = true;
        }
    }

    void ChooseTexts(int index)
    {
        popUpText = Resources.Load<TextAsset>("PopUpTexts");
        string[] popUpSorok = popUpText.text.Split('\n');
        popUpMessage = popUpSorok[index].Trim();
        if (!onlypopUp)
        {
            popUpText = Resources.Load<TextAsset>("PopUpTexts");
            popUpSorok = popUpText.text.Split('\n');
            popUpMessage = popUpSorok[index-1].Trim();
            onlypopUp = true;
        }

        objectiveText = Resources.Load<TextAsset>("ObjectiveTexts");
        string[] objectSorok = objectiveText.text.Split('\n');
        objMessage = objectSorok[index].Trim();

        speechText = Resources.Load<TextAsset>("SpeechTexts");
        string[] speechSorok = speechText.text.Split('\n');
        speechMessage = speechSorok[index].Trim();

    }
}
