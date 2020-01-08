using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorIsOpening : MonoBehaviour
{
    public GameObject door;
    public Animator doorIsOpening;
    void Start()
    {

        door = GameObject.FindGameObjectWithTag("RootRoom").transform.GetChild(1).gameObject.transform.GetChild(3).gameObject;
        doorIsOpening = door.GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        doorIsOpening.SetBool("OpenTheDoor", true);
        door.transform.GetChild(1).gameObject.transform.GetChild(4).gameObject.SetActive(true);
    }
}
