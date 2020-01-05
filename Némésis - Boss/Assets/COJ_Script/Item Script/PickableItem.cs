using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : MonoBehaviour
{
    public PlayerControllerScript playerC;
    public GameManagerScript gameManager;


    public Item item;
    protected bool canPick = true;
    private void Start()
    {
        playerC = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControllerScript>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>();
    }

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
        if (gameObject.CompareTag("ShopItem"))
        {
            int cost = 40;
            if(gameManager.playerMoney >= cost)
            {
                gameManager.playerMoney -= cost;
                Inventory.instance.Add(item);
                Destroy(gameObject);
            }
            Debug.Log("item bought");
        }
        else if (gameObject.CompareTag("ShopWeapon"))
        {
            int cost = 100;
            if(gameManager.playerMoney >= cost)
            {
                gameManager.playerMoney -= cost;
                Inventory.instance.Add(item);
                Destroy(gameObject);
            }
            Debug.Log("weapon bought");
        }  
        else if (gameObject.CompareTag("Item"))
        {
            Inventory.instance.Add(item);
            Destroy(gameObject);
        }
        Debug.Log("item picked up");
    }
}
