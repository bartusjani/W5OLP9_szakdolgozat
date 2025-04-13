using UnityEngine;

public class ControllerSpeechBubble : MonoBehaviour
{
    public SpeechBubble prefab;
    public string message;

    private SpeechBubble activeBubble;

    bool isPlayerInTrigger = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
            if (activeBubble == null)
            {
                Transform parent = GameObject.Find("PlayerSpeechBubbles").transform;

                activeBubble = Instantiate(prefab, parent);
                activeBubble.SetText(message);
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
        }
    }
}
