using UnityEngine;

public class PlayerLedgeGrab : MonoBehaviour
{
    private Rigidbody2D rb;
    private float startingGravity;
    public GameObject trigger;

    void Start()
    {
        
    }

    void Update()
    {
        CheckLedgeGrab();
    }

    void CheckLedgeGrab()
    {
        if (trigger.CompareTag("Player"))
        {
            Debug.Log("AAAAAAAA");
        }
    }


}
