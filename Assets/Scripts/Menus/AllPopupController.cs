using System.Collections;
using UnityEngine;
using UnityEngine.TextCore.Text;
using TextAsset = UnityEngine.TextAsset;

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

    TextAsset popUpTexts;
    string popUpText = "";

    TextAsset objectiveTexts;
    string objectiveText = "";

    TextAsset speechTexts;
    string speechText = ""; 


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
                    ChooseTexts(0);
                    StartCoroutine(SetPopUp(popUpText,objectiveText, speechText));
                }
                else
                {
                    if (activeBubble == null && objActiveBubble == null)
                    {
                        ChooseTexts(1);
                        StartCoroutine(SetPopUp(popUpText,objectiveText));
                    }
                }
            }
        }
        if (collision.CompareTag("Player"))
        {

            isPlayerInTrigger = true;
            if (!wasSpeaking)
            {
                ChooseTexts(0);
                StartCoroutine(SetPopUp(popUpText, objectiveText, speechText));
            }
            else
            {
                if (activeBubble == null && objActiveBubble == null)
                {
                    ChooseTexts(0);
                    StartCoroutine(SetPopUp(popUpText, objectiveText));
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
            activeBubble.SetText(message);


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

            yield return new WaitForSeconds(0.2f);
            SetObjective(objMessage);

            yield return new WaitForSeconds(0.2f);
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

    void ChooseTexts(int index)
    {

        objectiveTexts = Resources.Load<TextAsset>("ObjectiveTexts");
        string[] objectSorok = objectiveTexts.text.Split('\n');
        objectiveText = objectSorok[index].Trim();

        speechTexts = Resources.Load<TextAsset>("SpeechTexts");
        string[] speechSorok = speechTexts.text.Split('\n');
        speechText = speechSorok[index].Trim();

        popUpTexts = Resources.Load<TextAsset>("PopUpTexts");
        string[] popUpSorok = popUpTexts.text.Split('\n');
        popUpText = popUpSorok[index].Trim();

    }
}
