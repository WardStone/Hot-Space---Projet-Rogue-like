using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStat : MonoBehaviour
{
    public PlayerControllerScript playerC;
    public Inventory playerInventory;
    public Restart restart;
    public Item newItem;
    public int listLenght = 0;
    public Animator legAnimator;
    public Animator torsoAnimator;

    public float playerHealth; //
    public float maxHealth;
    protected int damageTaken;
    protected bool canTakeDamage = true;
    protected float privateTimer;
    public Slider playerBar;
    public Text playerHealthValue;

    public float howManybulleShot; // nb de balle par tir   //
    public float bulletLifeSpan; // portée de la balle //
    public float delayBeforeFirstShot;// délai avant le premier tir //
    public float delayBeforeNextShot;//délai avant le prochain tir //
    public float bulletSpeed;//vitesse de la balle //
    public float bulletDamage;//dégat de la balle //
    public float weaponAccuracy;//Précision de l'arme //
    public float playerSpeed;//vitesse du joueur //
    public GameObject bulletPrefab;

    public float howManyBulletShotSave;
    public float bulletLifeSpanSave;
    public float delayBeforeFirstShotSave;
    public float delayBeforeNextShotSave;
    public float bulletSpeedSave;
    public float bulletDamageSave;
    public float weaponAccuracySave;
    public float playerSpeedSave;
    public float playerHealthSave;

    public Color DamagedColor;
    public Color NormalColor;
    public GameObject TorsoRenderer;
    public GameObject LegRenderer;
    public Image redScreenEffect;

    public bool canGetStat;
    public bool isDead = false;

    public Item defaultWeapon;


    // Start is called before the first frame update
    public void Start()
    {
        playerC = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControllerScript>();
        TorsoRenderer = GameObject.FindGameObjectWithTag("Torso");
        LegRenderer = GameObject.FindGameObjectWithTag("Legs");
        torsoAnimator = GameObject.FindGameObjectWithTag("Torso").GetComponent<Animator>();
        legAnimator = GameObject.FindGameObjectWithTag("Legs").GetComponent<Animator>();
        redScreenEffect = GameObject.FindGameObjectWithTag("RedScreenEffect").GetComponent<Image>();
        restart = gameObject.GetComponent<Restart>();


        howManybulleShot = defaultWeapon.howManybulleShot;
        bulletLifeSpan = defaultWeapon.bulletLifeSpan;
        delayBeforeFirstShot = defaultWeapon.delayBeforeFirstShot;
        delayBeforeNextShot = defaultWeapon.delayBeforeNextShot;
        bulletSpeed = defaultWeapon.bulletSpeed;
        bulletDamage = defaultWeapon.bulletDamage;
        weaponAccuracy = defaultWeapon.weaponAccuracy;
        bulletPrefab = defaultWeapon.bulletPrefab;
        playerHealth= defaultWeapon.healthBonus;
        maxHealth = defaultWeapon.healthBonus;
        playerSpeed = 5;
        redScreenEffect.enabled = false;

        howManyBulletShotSave = 1;
        bulletLifeSpanSave = 1;
        delayBeforeFirstShotSave = 0;
        delayBeforeNextShotSave = 1;
        bulletSpeedSave = 1;
        bulletDamageSave = 1;
        weaponAccuracySave = 1;
        playerSpeedSave = 1;
      
        NewPassiveAcquired();

    }

    private void Update()
    {
        GetItem();
        playerBar.maxValue = maxHealth;
        playerBar.value = playerHealth;
        playerHealthValue.text = playerHealth.ToString();

        if (playerHealth > maxHealth)
        {
            playerHealth = maxHealth;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("bossLeftArm01") || other.CompareTag("bossLeftArm02") || other.CompareTag("bossLeftArm03") && canTakeDamage == true)
        {
            canTakeDamage = false;
            damageTaken = 25;
            privateTimer = 1.5f;
            StartCoroutine(takeDamage());
        }

        if (other.CompareTag("bossRightArm01") || other.CompareTag("bossRightArm02") || other.CompareTag("bossRightArm03") && canTakeDamage == true)
        {
            canTakeDamage = false;
            damageTaken = 25;
            privateTimer = 1.5f;
            StartCoroutine(takeDamage());
        }

        if (other.CompareTag("EnemyBullet") && canTakeDamage == true)
        {
            canTakeDamage = false;
            damageTaken = 10;
            privateTimer = 0.2f;
            StartCoroutine(takeDamage());
        }

        if (other.CompareTag("BossBullet") || other.CompareTag("HomingBossBullet") || other.CompareTag("BossRandomBullet") || other.CompareTag("BossBouncyBullet") || other.CompareTag("BossRockBullet") && canTakeDamage == true)
        {
            canTakeDamage = false;
            damageTaken = 20;
            privateTimer = 0.5f;
            StartCoroutine(takeDamage());
        }

        if (other.CompareTag("BrkRock") && canTakeDamage == true)
        {
            canTakeDamage = false;
            damageTaken = 20;
            privateTimer = 0.5f;
            StartCoroutine(takeDamage());
        }

        if (other.CompareTag("Enemy") && canTakeDamage == true)
        {
            canTakeDamage = false;
            damageTaken = 15;
            privateTimer = 2f;
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
        StartCoroutine(HurtColor());
        if (playerHealth <= 0 && isDead == false)
        {
            isDead = true;
            StartCoroutine(DeathAndRestart());
            Debug.Log("wtf");
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
        howManyBulletShotSave += newItem.howManybulleShot;
        Debug.Log("howManyBulletShot is now" + howManybulleShot);
        bulletLifeSpan += bulletLifeSpan * newItem.bulletLifeSpan;
        bulletLifeSpanSave += bulletLifeSpanSave * newItem.bulletLifeSpan;
        Debug.Log("bulletLifeSap is now" + bulletLifeSpan);
        delayBeforeFirstShot += newItem.delayBeforeFirstShot;
        delayBeforeFirstShotSave += newItem.delayBeforeFirstShot;
        Debug.Log("delayBeforeFirstShot is now" + delayBeforeFirstShot);
        delayBeforeNextShot += delayBeforeNextShot * newItem.delayBeforeNextShot;
        delayBeforeNextShotSave += delayBeforeNextShotSave * newItem.delayBeforeNextShot;
        Debug.Log("delayBeforeNextShot is now" + delayBeforeNextShot);
        bulletSpeed += bulletSpeed * newItem.bulletSpeed;
        bulletSpeedSave += bulletSpeedSave * newItem.bulletSpeed;
        Debug.Log("bulletSpeed is now" + bulletSpeed);
        bulletDamage += bulletDamage * newItem.bulletDamage;
        bulletDamageSave += bulletDamageSave * newItem.bulletDamage;
        Debug.Log("Damage is now" + bulletDamage);
        weaponAccuracy += weaponAccuracy * newItem.weaponAccuracy;
        weaponAccuracySave += weaponAccuracy * newItem.weaponAccuracy;
        Debug.Log("Accuracy is  now" + weaponAccuracy);
        playerHealth += newItem.healthBonus;
        playerHealthSave += newItem.healthBonus;
        maxHealth += newItem.healthBonus;
        Debug.Log("PlayerHealth is now" + playerHealth);
        playerSpeed += playerSpeed * newItem.playerSpeed;
        playerSpeedSave += playerSpeedSave * newItem.playerSpeed;
        Debug.Log("playerSpeed is now" + playerSpeed);
        NewPassiveAcquired();
        yield return null;

    }

    IEnumerator SetStatFromWeapon()
    {
        howManybulleShot = newItem.howManybulleShot + howManyBulletShotSave;
        bulletLifeSpan = newItem.bulletLifeSpan * bulletLifeSpanSave;
        delayBeforeFirstShot = newItem.delayBeforeFirstShot + delayBeforeFirstShotSave;
        delayBeforeNextShot = newItem.delayBeforeNextShot * delayBeforeNextShotSave;
        bulletSpeed = newItem.bulletSpeed * bulletSpeedSave;
        bulletDamage = newItem.bulletDamage * bulletDamageSave;
        weaponAccuracy = newItem.weaponAccuracy * weaponAccuracySave;
        bulletPrefab = newItem.bulletPrefab;
        NewPassiveAcquired();
        yield return null;
        Debug.Log("You got the weapon " + newItem.name);
    }

    void NewPassiveAcquired()
    {
        if (defaultWeapon.weaponRef == 1)
        {
            playerC.laserScope = true;
        }
        else
        {
            playerC.laserScope = false;
        }
    }

    IEnumerator HurtColor()
    {
        LegRenderer.GetComponent<SpriteRenderer>().color = DamagedColor;
        TorsoRenderer.GetComponent<SpriteRenderer>().color = DamagedColor;
        redScreenEffect.enabled = true;
        yield return new WaitForSeconds(0.2f);
        LegRenderer.GetComponent<SpriteRenderer>().color = NormalColor;
        TorsoRenderer.GetComponent<SpriteRenderer>().color = NormalColor;
        redScreenEffect.enabled = false;
    }

    IEnumerator DeathAndRestart()
    {
        torsoAnimator.SetTrigger("dead");
        legAnimator.SetTrigger("dead");
        torsoAnimator.SetBool("isDead", true);
        legAnimator.SetBool("isDead", true);
        playerSpeed = 0;
        playerC.canMove = false;
        playerC.canDash = false;
        yield return new WaitForSeconds(3f);
        restart.RestartGame();
    }
}
