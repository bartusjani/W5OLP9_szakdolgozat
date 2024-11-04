using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public GameObject player;

    private Vector3 position;
    void Start()
    {
        position = transform.position;
    }

    void Update()
    {
        position.x = player.transform.position.x;
        transform.position = position;
    }
}
