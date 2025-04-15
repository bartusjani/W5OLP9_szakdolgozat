using UnityEngine;

public class PopUpCounter : MonoBehaviour
{
    private static PopUpCounter instance;
    public static PopUpCounter Instance => instance;

    public int textIndex = 1;
    public int lastTextIndex = -1;

    private void Awake()
    {
        if (Instance == null)
        {
            instance = this;
        }
    }

}
