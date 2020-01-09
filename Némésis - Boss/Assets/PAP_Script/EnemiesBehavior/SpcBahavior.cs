using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpcBahavior : MonoBehaviour
{
    public PlayerStat playerStat;
    public GameManagerScript gameManager;

    public float health;
    public float speed;
    public float dmgBoxSpawnDelay;
    public GameObject spcDmgBox;

    public Transform[] target1;
    public Transform[] target2;

    private int currentWaypoint;
    private int previousPath;
    private int currentPath = 1;
    private bool canChangePath = true;
    private bool canMove = true;

    protected Rigidbody2D spcRb;
    protected Vector2 dirVector;

    private Animator anim;

    public Color hurtColor;
    public Color normalColor = Color.white;


    // Start is called before the first frame update
    void Start()
    {
        playerStat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStat>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>();
        InvokeRepeating("SpawnDamageBox", 0, dmgBoxSpawnDelay);
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {



        if (transform.position != target1[currentWaypoint].position && currentPath == 1 && canMove == true)
        {

            Vector2 pos = Vector2.MoveTowards(transform.position, target1[currentWaypoint].position, speed * Time.deltaTime);
            GetComponent<Rigidbody2D>().MovePosition(pos);

            //Vector2 dir = new Vector2(target1[currentPath].position.x - target1[previousPath].position.x, target1[currentPath].position.y - target1[previousPath].position.y);

            dirVector = (transform.position - target1[currentWaypoint].position).normalized;
            anim.SetFloat("MoveX", dirVector.x);
            anim.SetFloat("MoveY", dirVector.y);

            //if (dir.x > 0)
            //{
            //    anim.SetBool("isMovingRight", false);
            //    anim.SetBool("isMovingLeft", true);
            //}
            //else
            //{
            //    anim.SetBool("isMovingRight", true);
            //    anim.SetBool("isMovingLeft", false);
            //}
        }

        else if (transform.position != target2[currentWaypoint].position && currentPath == 2 && canMove == true)
        {
            Vector2 pos = Vector2.MoveTowards(transform.position, target2[currentWaypoint].position, speed * Time.deltaTime);
            GetComponent<Rigidbody2D>().MovePosition(pos);

            dirVector = (transform.position - target2[currentWaypoint].position).normalized;
            anim.SetFloat("MoveX", dirVector.x);
            anim.SetFloat("MoveY", dirVector.y);
        }


        else
        {
            previousPath = currentPath;


            if (target1[currentWaypoint].CompareTag("Branch") && canChangePath == true || target2[currentWaypoint].CompareTag("Branch") && canChangePath == true)
            {
                currentPath = Random.Range(1, 3);
                Debug.Log("le current path est : " + currentPath);
                StartCoroutine("NextWaypoint");
            }

            if (currentPath == previousPath)
            {
                currentWaypoint = (currentWaypoint + 1) % target1.Length;
            }

            if (currentPath != previousPath)
            {
                currentWaypoint = (currentWaypoint) % target2.Length;
            }


        }

    }

    IEnumerator NextWaypoint()
    {
        canChangePath = false;
        yield return new WaitForSeconds(0.5f);
        canChangePath = true;
    }

    private void SpawnDamageBox()
    {
        Instantiate(spcDmgBox, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Bullet"))
        {
           StartCoroutine(enemyTakeDamage());
        }

    
    }

  

    IEnumerator enemyTakeDamage()
    {
        health -= playerStat.bulletDamage;
        gameObject.GetComponent<SpriteRenderer>().color = hurtColor;
        Debug.Log("enemy has taken " + playerStat.bulletDamage);
        if (health <= 0)
        {
            CancelInvoke("SpawnDamageBox");
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
        int loot = Random.Range(5, 7);
        gameManager.playerMoney += loot;
    }

}
