using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossDoorDétection : MonoBehaviour
{
    public int keyLeft;
    public GameObject theDoor;
    public bool boiInTheRoom;

    void Start()
    {
        theDoor = GameObject.FindGameObjectWithTag("RootRoom").transform.GetChild(2).gameObject;
        keyLeft = 2;
    }

    void Update()
    {
        if (boiInTheRoom)
        {
            if (GameObject.FindGameObjectsWithTag("LeftKey").Length == 0 && GameObject.FindGameObjectsWithTag("RightKey").Length == 1)
            {
                keyLeft = 1;
                theDoor.transform.GetChild(2).gameObject.SetActive(true);
            }
            if (GameObject.FindGameObjectsWithTag("RightKey").Length == 0 && GameObject.FindGameObjectsWithTag("LeftKey").Length == 1)
            {
                keyLeft = 1;
                theDoor.transform.GetChild(1).gameObject.SetActive(true);
            }

            if (GameObject.FindGameObjectsWithTag("RightKey").Length == 0 && GameObject.FindGameObjectsWithTag("LeftKey").Length == 0)
            {
                keyLeft = 0;

                theDoor.transform.GetChild(1).gameObject.SetActive(false);
                theDoor.transform.GetChild(2).gameObject.SetActive(false);
                theDoor.transform.GetChild(3).gameObject.SetActive(true);
            }
            if (keyLeft == 0)
                theDoor.transform.GetChild(4).gameObject.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            boiInTheRoom = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            boiInTheRoom = true;
        }
    }
}
