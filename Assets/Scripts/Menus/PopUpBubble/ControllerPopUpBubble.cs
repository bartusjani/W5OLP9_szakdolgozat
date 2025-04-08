using UnityEngine;

public class ControllerPopUpBubble : MonoBehaviour
{
    public PopUpBubble prefab;
    public string message;
    public Sprite image;
    private PopUpBubble activeBubble;

    public Trigger tr;
    bool isPlayerInTrigger = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")&& !tr.isDoorOpen)
        {
            isPlayerInTrigger = true;
            if (activeBubble == null)
            {
                Transform parent = GameObject.Find("PopUps").transform;

                activeBubble = Instantiate(prefab,parent);
                activeBubble.SetText(message);
                activeBubble.SetSprite(image);
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
