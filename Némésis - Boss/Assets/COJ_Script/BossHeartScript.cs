using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHeartScript : MonoBehaviour
{
    public BossPatternLoop boss;

    public Rigidbody2D bossCoreRb;
    public Vector2 bossCoreDirection;
    public float coreSpeed = 150f;
    // Start is called before the first frame update
    void Start()
    {
        boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossPatternLoop>();
        bossCoreDirection = Vector2.one.normalized;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (boss.bossHealth <= 500)
        {

            bossCoreRb.velocity = bossCoreDirection * coreSpeed * Time.deltaTime;
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            

        }
        if(boss.bossHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("It has triggered");
        if (collision.CompareTag("Bullet"))
        {
            if (boss.canTakeDamage == true)
            {
                StartCoroutine(boss.takeDamage());
            }
        }

        if (collision.CompareTag("WallY"))
        {
            bossCoreDirection.y = -bossCoreDirection.y;
        }

        if (collision.CompareTag("WallX"))
        {
            bossCoreDirection.x = -bossCoreDirection.x;
        }
    }
}
