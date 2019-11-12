using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : MonoBehaviour
{

    public Item item;
    protected bool canPick = true;
 
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Player") && Input.GetButtonDown("Interact"))
        {
            if(canPick == true)
            {
                PickUp();
                canPick = false;
            }
        
        }
    }

    void PickUp()
    {
        Debug.Log("Picking up" + item.name);
        Inventory.instance.Add(item);
        
    }
}
