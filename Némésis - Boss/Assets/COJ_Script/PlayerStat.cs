using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStat : MonoBehaviour
{

    public Inventory playerInventory;
    public Item newItem;
    public int listLenght = 0;

    public int playerHealth;
    protected int damageTaken;
    protected bool canTakeDamage = true;
    protected float privateTimer ;
    public Slider playerBar;

    public float howManybulleShot; // nb de balle par tir
    public float bulletLifeSpan; // portée de la balle
    public float delayBeforeFirstShot;// délai avant le premier tir
    public float delayBeforeNextShot;//délai avant le prochain tir
    public float bulletSpeed;//vitesse de la balle
    public Transform bulletSize;// taille de la balle
    public int bulletDamage;//dégat de la balle
    public float weaponAccuracy;//Précision de l'arme
    public float playerSpeed;//vitesse du joueur

    public bool canGetStat;

    public Item defaultWeapon;


    // Start is called before the first frame update
    public void Start()
    {
        howManybulleShot = defaultWeapon.howManybulleShot;
        bulletLifeSpan = defaultWeapon.bulletLifeSpan;
        delayBeforeFirstShot = defaultWeapon.delayBeforeFirstShot;
        delayBeforeNextShot = defaultWeapon.delayBeforeNextShot;
        bulletSpeed = defaultWeapon.bulletSpeed;
        bulletSize = defaultWeapon.bulletSize;
        bulletDamage = defaultWeapon.bulletDamage;
        weaponAccuracy = defaultWeapon.weaponAccuracy;
        playerHealth = 100;
        playerSpeed = 5;

    }

    private void Update()
    {
        GetItem();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("bossLeftArm01") && canTakeDamage == true)
        {
            damageTaken = 30;
            privateTimer = 0.5f;
            StartCoroutine(takeDamage());
            Debug.Log("Ouch");
        }

        if (other.CompareTag("bossRightArm01") && canTakeDamage == true)
        {
            damageTaken = 30;
            privateTimer = 0.5f;
            StartCoroutine(takeDamage());
            Debug.Log("Aie ouille");
        }

        if (other.CompareTag("EnemyBullet"))
        {
            damageTaken = 5;
            privateTimer = 1f;
            StartCoroutine(takeDamage());
        }


    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("bossHead01") && canTakeDamage == true)
        {
            damageTaken = 2;
            privateTimer = 0.1f;
            StartCoroutine(takeDamage());
            Debug.Log("Laser Beam");

        }

        if(other.CompareTag("damagedZone") && canTakeDamage == true)
        {
            damageTaken = 10;
            privateTimer = 0.5f;
            StartCoroutine(takeDamage());
            Debug.Log("ça brûle baby boi");
        }
    }

    IEnumerator takeDamage()
    {
        playerHealth -= damageTaken;
        playerBar.value = playerHealth;

        if (playerHealth <= 0)
        {
            Destroy(gameObject);
        }

        Debug.Log("Player health equal" + playerHealth);
        canTakeDamage = false;
        yield return new WaitForSeconds(privateTimer);
        canTakeDamage = true;

    }

    void GetItem()
    {
        if (listLenght != playerInventory.items.Count)
        {
            newItem = playerInventory.items[playerInventory.items.Count -1];
            listLenght++;
            StartCoroutine(GetStatFromItem());
            Debug.Log("List lenght =" + listLenght);
        }
    }

    IEnumerator GetStatFromItem()
    {
        howManybulleShot += newItem.howManybulleShot;
        bulletLifeSpan += newItem.bulletLifeSpan;
        delayBeforeFirstShot += newItem.delayBeforeFirstShot;
        delayBeforeNextShot += defaultWeapon.delayBeforeNextShot;
        bulletSpeed += defaultWeapon.bulletSpeed;
        //bulletSize = defaultWeapon.bulletSize;
        bulletDamage += defaultWeapon.bulletDamage;
        weaponAccuracy += defaultWeapon.weaponAccuracy;
        playerHealth += newItem.healthBonus;
        playerSpeed += newItem.playerSpeed;
        yield return null;
        Debug.Log("bullet damage =" + bulletDamage);
    }

}
