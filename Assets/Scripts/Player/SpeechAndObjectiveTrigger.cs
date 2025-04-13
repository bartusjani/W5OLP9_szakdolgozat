using UnityEngine;

public class SpeechAndObjectiveTrigger : MonoBehaviour
{
    public SpeechBubble prefab;
    private SpeechBubble activeBubble;
    public string message;


    public PopUpBubble objPrefab;
    private PopUpBubble objActiveBubble;
    public string objMessage;

    bool wasSpeaking = false;
    private bool isPlayerInTrigger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
            SetSpeech(message);
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
        if (activeBubble != null)
        {
            Destroy(activeBubble.gameObject);
            activeBubble = null;
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
        if (activeBubble == null && !wasSpeaking)
        {
            Transform parent = GameObject.Find("PlayerSpeechBubbles").transform;

            activeBubble = Instantiate(prefab, parent);
            activeBubble.SetText(message);
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
}
