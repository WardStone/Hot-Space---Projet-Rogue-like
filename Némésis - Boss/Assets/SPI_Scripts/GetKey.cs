using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetKey : MonoBehaviour
{
    public bool canPick=false;
    public Text description;
   
    void Update()
    {
        if (canPick && Input.GetButtonDown("Interact"))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            description.enabled = true;
            canPick = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            description.enabled = false;
            canPick = false;
        }
    }
}
