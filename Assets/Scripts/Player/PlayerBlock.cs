using System.Collections;
using UnityEngine;

public class PlayerBlock : MonoBehaviour
{
    public GameObject shield;

    public float blockDur = 2f;
    public float blockCooldown = 3f;

    private bool canBlock = true;
    private bool isBlocking = false;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab)&& canBlock)
        {
            StartCoroutine(Block());
        }


    }
    private IEnumerator Block()
    {
        canBlock = false;
        isBlocking = true;
        shield.SetActive(true);

        yield return new WaitForSeconds(blockDur);

        shield.SetActive(false);
        isBlocking=false;

        yield return new WaitForSeconds(blockCooldown);
        canBlock = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isBlocking && collision.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
        }
    }
}
