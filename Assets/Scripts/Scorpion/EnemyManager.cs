using System.Xml.Schema;
using UnityEngine;

public class GroundDoorTrigger : MonoBehaviour
{
    public GameObject groundDoor;
    private int allScorpions;
    private int deadScorpions = 0;


    private void Start()
    {
        allScorpions = GameObject.FindGameObjectsWithTag("Scorpion").Length;
        //Debug.Log("Scorpions:" + allScorpions);

        EnemyHealth.OnAnyEnemyDeath += EnemyDeathHandler;

    }

    private void EnemyDeathHandler()
    {
        deadScorpions++;
        //Debug.Log("Enemy remaining:" + (allScorpions - deadScorpions));

        if(deadScorpions>= allScorpions)
        {
            //Debug.Log("All died");
            groundDoor.SetActive(false);
        }
    }

    
    private void OnDestroy()
    {
        EnemyHealth.OnAnyEnemyDeath -= EnemyDeathHandler;
    }


}
