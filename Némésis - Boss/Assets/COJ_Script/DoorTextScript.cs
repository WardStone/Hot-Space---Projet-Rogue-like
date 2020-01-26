using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorTextScript : MonoBehaviour
{
    public Text doorText;
    public BossDoorDétection key;
    // Start is called before the first frame update
    void Start()
    {
        doorText.enabled = false;
        key = GameObject.Find("Door").GetComponent<BossDoorDétection>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && key.keyLeft > 0)
        {
            doorText.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            doorText.enabled = false;
        }
    }
}
