using UnityEngine;

public class StaticTrigger : MonoBehaviour
{
    public GameObject statEnemy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Player")statEnemy.SetActive(true);
    }
}
