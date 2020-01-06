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
            ennemyNumber = GameObject.FindGameObjectsWithTag("Enemy").Length;
            if (ennemyNumber <= 0)
                tpManager.gameObject.tag = "Untagged";
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasBeenActivated == false)
        {
            tpManager.gameObject.tag = ("lock");
            currentRoom.transform.GetChild(1).gameObject.SetActive(true);
            hasBeenActivated = true;
        }
    }
}
