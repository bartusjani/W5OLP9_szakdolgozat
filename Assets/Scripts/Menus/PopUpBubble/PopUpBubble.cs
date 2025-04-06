using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopUpBubble : MonoBehaviour
{
    public TMP_Text PopUpText;
    public Image sprite;

    public void SetText(string text)
    {
        PopUpText.text = text;
    }
    public void SetSprite(Sprite s)
    {
        sprite.sprite = s;
    }
}
