using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CacBehavior : MonoBehaviour
{
    public PlayerStat playerStat;
    public GameManagerScript gameManager;

    public float health;
    public float normalSpeed;
    public float dashSpeed;
    public float stoppingDistance;

    public float recoveryTime;
    public float timeBeforeDash;

    private bool canMove = true;
    private bool stopDash = false;

    [HideInInspector]
    public Transform player;

    private Vector3 target;

    public Color dashColor = Color.red;
    public Color normalColor = Color.white;
     public Color hurtColor;


    private Animator anim;

    


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        playerStat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStat>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, player.position) > stoppingDistance && canMove == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, normalSpeed * Time.deltaTime);
        }

        else if (Vector2.Distance(transform.position, player.position) <= stoppingDistance && canMove == true)
        {
            canMove = false;
            anim.SetBool("isMoving", false);


            StartCoroutine("DashAttack");
        }

    }

    IEnumerator DashAttack()
    {

        GetComponent<SpriteRenderer>().material.color = dashColor;
        
        yield return new WaitForSeconds(timeBeforeDash);

        target = new Vector2(player.position.x, player.position.y);

        anim.SetBool("isJumping", true);

        while (transform.position != target && stopDash == false)
        {
            

            GetComponent<CircleCollider2D>().isTrigger = true;
            transform.position = Vector2.MoveTowards(transform.position, target, dashSpeed * Time.deltaTime);

            yield return new WaitForSeconds(0.005f);

        }

        anim.SetBool("isJumping", false);

        GetComponent<CircleCollider2D>().isTrigger = false;

        stopDash = false;

        GetComponent<SpriteRenderer>().material.color = normalColor;

        yield return new WaitForSeconds(recoveryTime);

        canMove = true;
        anim.SetBool("isMoving", true);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
           StartCoroutine(enemyTakeDamage());
        }

        if (other.CompareTag("Wall"))
        {
            stopDash = true;
        }
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            stopDash = true;
        }
    }

    IEnumerator enemyTakeDamage()
    {
        health -= playerStat.bulletDamage;
        gameObject.GetComponent<SpriteRenderer>().color = hurtColor;

        if (health <= 0)
        {
            anim.SetBool("isDead", true);
            canMove = false;
        }
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<SpriteRenderer>().color = normalColor;
    }

    private void Death()
    {
        GetMoney();
        Destroy(gameObject);
    }

    void GetMoney()
    {
        int loot = Random.Range(5, 15);
        gameManager.playerMoney += loot;
    }

}
