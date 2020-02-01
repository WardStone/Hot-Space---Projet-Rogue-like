using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorIsOpening : MonoBehaviour
{
    public GameObject door;
    public Animator doorIsOpening;
    private GameObject doorSound;
    void Start()
    {
        doorSound = GameObject.Find("DoorSound");
        door = GameObject.FindGameObjectWithTag("RootRoom").transform.GetChild(2).gameObject.transform.GetChild(3).gameObject;
        doorIsOpening = door.GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        doorIsOpening.SetBool("OpenTheDoor", true);
        doorSound.GetComponent<AudioSource>().enabled = true;
        door.transform.GetChild(1).gameObject.transform.GetChild(4).gameObject.SetActive(true);
    }
}
