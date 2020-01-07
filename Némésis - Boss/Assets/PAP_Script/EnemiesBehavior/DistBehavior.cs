using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistBehavior : MonoBehaviour
{
    public float health;
    public float speed;
    public float stoppingDistance;
    public float retreatDistance;

    public int shotNbr;
    public float startShootTime;
    public float recoveryTime;
    public float timeBtwShot;
    public float reloadTime;
    
    private bool canMove = true;
    private bool canShoot = true;
    bool isRight = true;

    [HideInInspector]
    public Transform player;

    public GameObject enemyBullet;

    public Color shootColor = Color.red;
    public Color normalColor = Color.white;

    private Animator anim;
    [SerializeField]
    Rigidbody2D body;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, player.position) > stoppingDistance && canMove == true)
        {
            body.MovePosition(Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime));

            /*if(player.position.x < transform.position.x)
            {
                anim.SetBool("isMovingRight", false);
            }
            else
            {
                anim.SetBool("isMovingRight", true);
            }*/
        }

        else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && canMove == true && canShoot == true)
        {
            canMove = false;
            canShoot = false;
            body.velocity = Vector2.zero;
            StartCoroutine("Shoot");
        }

       else if (Vector2.Distance(transform.position, player.position) < retreatDistance && canMove == true && canShoot == false)
       {
            body.MovePosition(Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime));

            /*if (player.position.x < transform.position.x)
            {
                anim.SetBool("isMovingRight", true);
            }
            else
            {
                anim.SetBool("isMovingRight", false);
            }*/
        }
       
    }

    IEnumerator Shoot()
    {

        yield return new WaitForSeconds(startShootTime);

        anim.SetBool("isAttacking", true);

        for (int i = 0; shotNbr > i; i++)
        {
            Instantiate(enemyBullet, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(timeBtwShot);
        }

        anim.SetBool("isAttacking", false);

        yield return new WaitForSeconds(recoveryTime);

        canMove = true;

        yield return new WaitForSeconds(reloadTime);

        canShoot = true;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            health -= 5;
        }

        if (health <= 0)
        {
            canMove = false;
            anim.SetBool("isDead", true);
        }
    }

    private void Death()
    {
        Destroy(gameObject);
    }

}
