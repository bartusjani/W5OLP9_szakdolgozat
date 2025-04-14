using UnityEngine;

public class SpeechAndObjectiveTrigger : MonoBehaviour
{
    public SpeechBubble speechPrefab;
    private SpeechBubble speechActiveBubble;
    string speechMessage;


    public PopUpBubble objPrefab;
    private PopUpBubble objActiveBubble;
    string objMessage;

    TextAsset objectiveText;
    TextAsset speechText;

    bool wasSpeaking = false;
    private bool isPlayerInTrigger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
            ChooseTexts(0);
            SetSpeech(speechMessage);
            SetObj(objMessage);
        }
        else
        {
            isPlayerInTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isPlayerInTrigger = false;
        if (speechActiveBubble != null)
        {
            Destroy(speechActiveBubble.gameObject);
            speechActiveBubble = null;
        }
        if(objActiveBubble!=null)
        {
            Destroy(objActiveBubble.gameObject);
            objActiveBubble = null;
        }
        Destroy(gameObject);
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

    void SetObj(string message)
    {
        if (objActiveBubble == null)
        {
            Transform parent = GameObject.Find("PopUps").transform;

            objActiveBubble = Instantiate(objPrefab, parent);
            objActiveBubble.SetText(message);
        }
    }


    void ChooseTexts(int index)
    {

        objectiveText = Resources.Load<TextAsset>("ObjectiveTexts");
        string[] objectSorok = objectiveText.text.Split('\n');
        objMessage = objectSorok[index].Trim();

        speechText = Resources.Load<TextAsset>("SpeechTexts");
        string[] speechSorok = speechText.text.Split('\n');
        speechMessage = speechSorok[0].Trim();
    }
}
