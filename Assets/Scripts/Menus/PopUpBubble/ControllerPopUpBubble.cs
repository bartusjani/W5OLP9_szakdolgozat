using UnityEngine;

public class ControllerPopUpBubble : MonoBehaviour
{
    public PopUpBubble prefab;
    public string message;
    public Sprite image;

    private PopUpBubble activeBubble;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (activeBubble == null)
            {
                Transform parent = GameObject.Find("PopUps").transform;

                activeBubble = Instantiate(prefab,parent);
                activeBubble.SetText(message);
                activeBubble.SetSprite(image);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (activeBubble != null)
            {
                Destroy(activeBubble.gameObject);
                activeBubble = null;
            }
        }
    }
}
