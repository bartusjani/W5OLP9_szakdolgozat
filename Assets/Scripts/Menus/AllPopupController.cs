using System.Collections;
using UnityEngine;
using UnityEngine.TextCore.Text;
using TextAsset = UnityEngine.TextAsset;

public class AllPopupController : MonoBehaviour
{

    public PopUpBubble popPrefab;
    string popMessage;


    public ObjectiveBubble objPrefab;
    string objMessage;


    public SpeechBubble speechPrefab;
    string speechMessage;


    private PopUpBubble activeBubble;
    private ObjectiveBubble objActiveBubble;
    private SpeechBubble speechActiveBubble;

    public GameObject platformerHelp;
    int helpCounter = 0;

    TextAsset popUpTexts;
    string popUpText = "";

    TextAsset objectiveTexts;
    string objectiveText = "";
    int objCounter = 0;

    TextAsset speechTexts;
    string speechText = ""; 


    public Trigger tr;
    bool isPlayerInTrigger = false;
    public bool wasSpeaking = false;


    private void Update()
    {
        if (PopUpCounter.Instance.textIndex != PopUpCounter.Instance.lastTextIndex)
        {
            RefreshBubbles();
            PopUpCounter.Instance.lastTextIndex = PopUpCounter.Instance.textIndex;
        }
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player") && !tr.isDoorOpen)
        {

            isPlayerInTrigger = true;
            if (!wasSpeaking)
            {
                ChooseTexts(PopUpCounter.Instance.textIndex);
                StartCoroutine(SetPopUp(popUpText,objectiveText, speechText));
            }
            else
            {
                if (activeBubble == null && objActiveBubble == null)
                {
                    ChooseTexts(PopUpCounter.Instance.textIndex);
                    if (objCounter == 1)
                    {
                        SetPopUp(popUpText);
                        objCounter = 0;
                    }
                    else
                    {
                        StartCoroutine(SetPopUp(popUpText, objectiveText));
                    }
                }
                wasSpeaking = false;
            }
        }
        
        if (collision.CompareTag("Player"))
        {

            isPlayerInTrigger = true;
            if (!wasSpeaking)
            {
                ChooseTexts(PopUpCounter.Instance.textIndex);
                StartCoroutine(SetPopUp(popUpText, objectiveText, speechText));
            }
            else
            {
                if (activeBubble == null && objActiveBubble == null)
                {
                    ChooseTexts(PopUpCounter.Instance.textIndex);
                    StartCoroutine(SetPopUp(popUpText, objectiveText));
                }
            }
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
        }
    }
    IEnumerator SetPopUp(string message ,string objMessage)
    {

        if (activeBubble == null)
        {
            SetPopUp(message);
            yield return new WaitForSeconds(1f);
            SetObjective(objMessage);


        }
    }

    IEnumerator SetPopUp(string message, string objMessage, string speechMessage)
    {
        
        if (activeBubble == null)
        {
            objCounter = 1;
            SetPopUp(message);
            yield return new WaitForSeconds(0.2f);
            SetSpeech(speechMessage);

            yield return new WaitForSeconds(0.2f);
            SetObjective(objMessage);
            wasSpeaking = true;
        }
    }

    void SetPopUp(string message)
    {

        if (activeBubble == null)
        {
            Transform parent = GameObject.Find("PopUps").transform;

            activeBubble = Instantiate(popPrefab, parent);
            activeBubble.SetText(message);
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
        popUpText = popUpSorok[index-1].Trim();


        Debug.Log($"textIndex: {index}");
        Debug.Log($"PopUpText: {popUpText}");
        Debug.Log($"ObjectiveText: {objectiveText}");
        Debug.Log($"SpeechText: {speechText}");
    }

    public void ClearAllBubbles()
    {
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
    }

    public void RefreshBubbles()
    {
        Debug.Log($"RefreshBubbles called for textIndex: {PopUpCounter.Instance.textIndex}");

        ChooseTexts(PopUpCounter.Instance.textIndex);
        ClearAllBubbles();
    }
}
