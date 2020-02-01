using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickableItem : MonoBehaviour
{
    public switchButton Abutton;
    public PlayerControllerScript playerC;
    public PlayerStat playerS;
    public GameManagerScript gameManager;
    public Text itemName;
    public Text itemDescription;
    public Text itemPrice;

    public GameObject ShopDenialSoundPrefab;
    public GameObject PickHealthPackSoundPrefab;
    public GameObject PickWeaponSoundPrefab;
    public GameObject PickItemSoundPrefab;


    public Item item;
    protected bool canPick = true;
    private void Start()
    {
        playerC = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControllerScript>();
        playerS = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStat>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>();
        itemName = gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        itemDescription = gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.GetComponent<Text>();
        itemPrice = gameObject.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.GetComponent<Text>();
        itemName.text = item.name;
        itemPrice.text = item.price;
        itemDescription.text = item.description;
        itemName.enabled = false;
        itemDescription.enabled = false;
        itemPrice.enabled = false;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
            if (other.CompareTag("Player") && Input.GetButtonDown("Interact"))
            {
                if (canPick == true)
                {
                    canPick = false;    
                    PickUp();    
                }

            }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Abutton = GameObject.Find("Abutton").GetComponent<switchButton>();
            Abutton.canInteract = true;
            itemName.enabled = true;
            itemDescription.enabled = true;
            if (gameObject.CompareTag("ShopItem") || gameObject.CompareTag("ShopWeapon") || gameObject.CompareTag("healthPack"))
            {
                itemPrice.enabled = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            itemName.enabled = false;
            itemDescription.enabled = false;
            itemPrice.enabled = false;
            Abutton.canInteract = false;
        }
    }
    void PickUp()
    {
        if (gameObject.CompareTag("ShopItem"))
        {
            int cost = 125;
            if(gameManager.playerMoney >= cost)
            {
                gameManager.playerMoney -= cost;
                Inventory.instance.Add(item);
                Destroy(gameObject);
                Instantiate(PickItemSoundPrefab);
            }
            else
            {
                Instantiate(ShopDenialSoundPrefab);
                canPick = true;
            }
            Debug.Log("item bought");
        }
        else if (gameObject.CompareTag("ShopWeapon"))
        {
            int cost = 200;
            if(gameManager.playerMoney >= cost)
            {
                gameManager.playerMoney -= cost;
                Inventory.instance.Add(item);
                Destroy(gameObject);
                Instantiate(PickWeaponSoundPrefab);
            }
            else
            {
                Instantiate(ShopDenialSoundPrefab);
                canPick = true;
            }
            Debug.Log("weapon bought");
        }  
        else if (gameObject.CompareTag("Item"))
        {
            Inventory.instance.Add(item);
            Destroy(gameObject);
            Instantiate(PickItemSoundPrefab);
        }
        else if (gameObject.CompareTag("healthPack"))
        {
            int healthPackCost = 30;
            if(gameManager.playerMoney >= healthPackCost)
            {
                gameManager.playerMoney -= healthPackCost;
                playerS.playerHealth += 30;
                Destroy(gameObject);
                Instantiate(PickHealthPackSoundPrefab);
            }
            else
            {
                Instantiate(ShopDenialSoundPrefab);
                canPick = true;
            }
        }
        Debug.Log("item picked up");
    }
}
