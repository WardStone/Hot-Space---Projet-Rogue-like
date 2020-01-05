using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDetection : MonoBehaviour
{

    public GameObject rootRoom;
    public GameObject tpManager;
    public GameObject currentRoom;

    void Start()
    {
        rootRoom = GameObject.FindGameObjectWithTag("RootRoom");
        tpManager = rootRoom.transform.GetChild(0).gameObject; // enfant de la salle racine qui sert juste de detecteur/ lock
    }

    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        currentRoom.transform.GetChild(0).gameObject.SetActive(true);
    }
}
