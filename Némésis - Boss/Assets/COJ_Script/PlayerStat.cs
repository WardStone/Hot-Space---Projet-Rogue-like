using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStat : MonoBehaviour
{
    public PlayerControllerScript playerC;
    public Inventory playerInventory;
    public Item newItem;
    public int listLenght = 0;

    public float playerHealth;
    protected int damageTaken;
    protected bool canTakeDamage = true;
    protected float privateTimer;
    public Slider playerBar;

    public float howManybulleShot; // nb de balle par tir
    public float bulletLifeSpan; // portée de la balle
    public float delayBeforeFirstShot;// délai avant le premier tir
    public float delayBeforeNextShot;//délai avant le prochain tir
    public float bulletSpeed;//vitesse de la balle
    public Transform bulletSize;// taille de la balle
    public float bulletDamage;//dégat de la balle
    public float weaponAccuracy;//Précision de l'arme
    public float playerSpeed;//vitesse du joueur
    public GameObject bulletPrefab;

    public bool canGetStat;

    public Item defaultWeapon;


    // Start is called before the first frame update
    public void Start()
    {
        playerC = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControllerScript>();




        howManybulleShot = defaultWeapon.howManybulleShot;
        bulletLifeSpan = defaultWeapon.bulletLifeSpan;
        delayBeforeFirstShot = defaultWeapon.delayBeforeFirstShot;
        delayBeforeNextShot = defaultWeapon.delayBeforeNextShot;
        bulletSpeed = defaultWeapon.bulletSpeed;
        bulletSize = defaultWeapon.bulletSize;
        bulletDamage = defaultWeapon.bulletDamage;
        weaponAccuracy = defaultWeapon.weaponAccuracy;
        bulletPrefab = defaultWeapon.bulletPrefab;
        playerHealth = 200;
        playerSpeed = 5;
        NewPassiveAcquired();

    }

    private void Update()
    {
        GetItem();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("bossLeftArm01") || other.CompareTag("bossLeftArm02") || other.CompareTag("bossLeftArm03") && canTakeDamage == true)
        {
            canTakeDamage = false;
            damageTaken = 30;
            privateTimer = 1.5f;
            StartCoroutine(takeDamage());
        }

        if (other.CompareTag("bossRightArm01") || other.CompareTag("bossRightArm02") || other.CompareTag("bossRightArm03") && canTakeDamage == true)
        {
            canTakeDamage = false;
            damageTaken = 30;
            privateTimer = 1.5f;
            StartCoroutine(takeDamage());
        }

        if (other.CompareTag("EnemyBullet") && canTakeDamage == true)
        {
            canTakeDamage = false;
            damageTaken = 5;
            privateTimer = 1.5f;
            StartCoroutine(takeDamage());
        }

        if (other.CompareTag("BossBullet") || other.CompareTag("HomingBossBullet") || other.CompareTag("BossRandomBullet") || other.CompareTag("BossBouncyBullet") || other.CompareTag("BossRockBullet") && canTakeDamage == true)
        {
            canTakeDamage = false;
            damageTaken = 10;
            privateTimer = 1.5f;
            StartCoroutine(takeDamage());
        }

        if (other.CompareTag("BrkRock") && canTakeDamage == true)
        {
            canTakeDamage = false;
            damageTaken = 15;
            privateTimer = 0.5f;
            StartCoroutine(takeDamage());
        }


    }

    private void OnTriggerStay2D(Collider2D other)
    {

        if (other.CompareTag("damagedZone") && canTakeDamage == true)
        {
            canTakeDamage = false;
            damageTaken = 10;
            privateTimer = 0.5f;
            StartCoroutine(takeDamage());
        }
    }

    IEnumerator takeDamage()
    {

        Debug.Log(damageTaken);
        playerHealth -= damageTaken;
        playerBar.value = playerHealth;

        if (playerHealth <= 0)
        {
            Destroy(gameObject);
        }

        Debug.Log("Player health equal" + playerHealth);
        yield return new WaitForSeconds(privateTimer);
        canTakeDamage = true;

    }

    void GetItem()
    {
        if (listLenght != playerInventory.items.Count)
        {
            newItem = playerInventory.items[playerInventory.items.Count - 1];
            listLenght++;

            if (newItem.isWeapon == false)
            {
                StartCoroutine(GetStatFromItem());
            }
            else if (newItem.isWeapon == true)
            {
                StartCoroutine(SetStatFromWeapon());
            }

            Debug.Log("List lenght =" + listLenght);
        }
    }

    IEnumerator GetStatFromItem()
    {
        Debug.Log("You got the item " + newItem.name);
        howManybulleShot = howManybulleShot * newItem.howManybulleShot;
        Debug.Log("howManyBulletShot is now" + howManybulleShot);
        bulletLifeSpan += newItem.bulletLifeSpan;
        Debug.Log("bulletLifeSap is now" + bulletLifeSpan);
        delayBeforeFirstShot += newItem.delayBeforeFirstShot;
        Debug.Log("delayBeforeFirstShot is now" + delayBeforeFirstShot);
        delayBeforeNextShot += newItem.delayBeforeNextShot;
        Debug.Log("delayBeforeNextShot is now" + delayBeforeNextShot);
        bulletSpeed += newItem.bulletSpeed;
        Debug.Log("bulletSpeed is now" + bulletSpeed);
        //bulletSize = defaultWeapon.bulletSize;
        bulletDamage += bulletDamage * newItem.bulletDamage;
        Debug.Log("Damage is now" + bulletDamage);
        weaponAccuracy += weaponAccuracy * newItem.weaponAccuracy;
        Debug.Log("Accuracy is  now" + weaponAccuracy);
        playerHealth += playerHealth * newItem.healthBonus;
        Debug.Log("PlayerHealth is now" + playerHealth);
        playerSpeed = playerSpeed * newItem.playerSpeed;
        Debug.Log("playerSpeed is now" + playerSpeed);
        NewPassiveAcquired();
        yield return null;

    }

    IEnumerator SetStatFromWeapon()
    {
        howManybulleShot = newItem.howManybulleShot;
        bulletLifeSpan = newItem.bulletLifeSpan;
        delayBeforeFirstShot = newItem.delayBeforeFirstShot;
        delayBeforeNextShot = newItem.delayBeforeNextShot;
        bulletSpeed = newItem.bulletSpeed;
        //bulletSize = defaultWeapon.bulletSize;
        bulletDamage = newItem.bulletDamage;
        weaponAccuracy = newItem.weaponAccuracy;
        bulletPrefab = newItem.bulletPrefab;
        NewPassiveAcquired();
        yield return null;
        Debug.Log("You got the weapon " + newItem.name);
    }

    void NewPassiveAcquired()
    {
        if (defaultWeapon.weaponRef == 1)
        {
            Debug.Log("WORK GODAMMIT");
            playerC.laserScope = true;
        }
    }
}
