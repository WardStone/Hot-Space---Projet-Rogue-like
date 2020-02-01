using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetKey : MonoBehaviour
{
    public switchButton Abutton;
    public bool canPick=false;
    public Text description;
    public GameObject pickKeySoundPrefab;

    void Update()
    {
        if (canPick && Input.GetButtonDown("Interact"))
        {
            Instantiate(pickKeySoundPrefab);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Abutton = GameObject.Find("Abutton").GetComponent<switchButton>();
            Abutton.canInteract = true;
            description.enabled = true;
            canPick = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Abutton.canInteract = false;
            description.enabled = false;
            canPick = false;
        }
    }
}
