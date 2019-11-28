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

    public Transform player;
    public GameObject enemyBullet;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, player.position) > stoppingDistance && canMove == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }

        else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && canMove == true && canShoot == true)
        {
            canMove = false;
            canShoot = false;
            StartCoroutine("Shoot");
        }

       else if (Vector2.Distance(transform.position, player.position) < retreatDistance && canMove == true && canShoot == false)
       {
           transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
       }
    }

    IEnumerator Shoot()
    {
        GetComponent<SpriteRenderer>().material.color = Color.red;

        yield return new WaitForSeconds(startShootTime);

        for (int i = 0; shotNbr > i; i++)
        {
            Instantiate(enemyBullet, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(timeBtwShot);
        }

        yield return new WaitForSeconds(recoveryTime);

        canMove = true;
        GetComponent<SpriteRenderer>().material.color = Color.white;

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
            Destroy(gameObject);
        }
    }

}
