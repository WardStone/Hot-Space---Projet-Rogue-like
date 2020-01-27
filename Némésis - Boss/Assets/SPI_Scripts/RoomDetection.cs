using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDetection : MonoBehaviour
{

    public GameObject rootRoom;
    public GameObject tpManager;
    public GameObject currentRoom;

    public int ennemyNumber;
    public bool hasBeenActivated;
    public bool containsEnemy;
    private IEnumerator test;

    void Start()
    {
        rootRoom = GameObject.FindGameObjectWithTag("RootRoom");
        tpManager = rootRoom.transform.GetChild(0).gameObject; // enfant de la salle racine qui sert juste de detecteur/ lock
        hasBeenActivated = false;
    }

    void Update()
    {
        if (hasBeenActivated)
        {
            if (containsEnemy)
            {


                ennemyNumber = GameObject.FindGameObjectsWithTag("Enemy").Length;
                if (ennemyNumber > 0)
                    test = Spawnez();
                StartCoroutine(test);

                if (ennemyNumber <= 0)
                {
                    containsEnemy = false;
                    tpManager.gameObject.tag = "Untagged";
                }

            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        /*if (hasBeenActivated == false & currentRoom.CompareTag("RootRoom"))
        {
            currentRoom.transform.GetChild(1).gameObject.SetActive(true);
            hasBeenActivated = true;
        }*/
        if (hasBeenActivated == false )
        {
            hasBeenActivated = true;
            currentRoom.transform.GetChild(4).gameObject.SetActive(true);
            StartCoroutine(timeSpawner());
            
        }
    }

    IEnumerator Spawnez()
    {
        tpManager.gameObject.tag = ("lock");
        if (GameObject.FindGameObjectsWithTag("Enemy").Length > 0)
        {
            containsEnemy = true;
        }
        yield break;
    }
    IEnumerator timeSpawner()
    {
        
        if (currentRoom.CompareTag("EnemyRoom"))
        { 
            tpManager.gameObject.tag = ("lock");
            currentRoom.transform.GetChild(3).gameObject.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            currentRoom.transform.GetChild(1).gameObject.SetActive(true);
            if (GameObject.FindGameObjectsWithTag("Enemy").Length > 0)
                containsEnemy = true;
        }
        else
        {
            currentRoom.transform.GetChild(1).gameObject.SetActive(true);
        }
        
    }

}
