using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletBehaviour : MonoBehaviour
{
    public BossPatternLoop boss;
    public Transform playerPos;
    public Transform headPos;
    protected Vector2 homingBulletDir;
    protected Rigidbody2D bulletRb;
    bool lockDir = false;
    Vector2 bounceBulletDir;
    Vector2 rockDir = Vector2.zero;
    bool setDir = true;

    public void Start()
    {
        boss = GameObject.Find("Boss").GetComponent<BossPatternLoop>();
        playerPos = GameObject.Find("Player").transform;
        headPos = GameObject.Find("HeadShotPoint").transform;
        homingBulletDir = playerPos.position - headPos.position;
        bounceBulletDir = new Vector2(Random.Range(-1f, 1), -1);

    }
    public void FixedUpdate()
    {
        bulletType();
    }

    public void bulletType()
    {
        if (gameObject.CompareTag("HomingBossBullet"))
        {
            StartCoroutine(BulletGosTowardPlayer());
        }

        if (gameObject.CompareTag("BossRandomBullet") && lockDir == false)
        {
            lockDir = true;
            StartCoroutine(BulletGoesRandom());
        }
        if (gameObject.CompareTag("BossBullet"))
        {
            StartCoroutine(BulletGoesForward());
        }
        if (gameObject.CompareTag("BossBouncyBullet"))
        {
            StartCoroutine(BulletBounceAround());
        }
        if (gameObject.CompareTag("BossRockBullet"))
        {
            StartCoroutine(RockBullet());
        }
    }
    IEnumerator BulletGosTowardPlayer()
    {
        float bulletSpeed = 450f;
        bulletRb = gameObject.GetComponent<Rigidbody2D>();
        bulletRb.velocity = homingBulletDir.normalized * bulletSpeed * Time.deltaTime;
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    IEnumerator BulletGoesRandom()
    {
        float bulletSpeed = 400f;
        Vector2 bulletDir = new Vector2(Random.Range(-1f,1f),-1f);
        bulletRb = gameObject.GetComponent<Rigidbody2D>();
        bulletRb.velocity = bulletDir * bulletSpeed * Time.deltaTime;
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    IEnumerator BulletGoesForward()
    {
        float bulletSpeed = 500f;
        Vector2 bulletDir = new Vector2(0, -1);
        bulletRb = gameObject.GetComponent<Rigidbody2D>();
        bulletRb.velocity = bulletDir.normalized * bulletSpeed * Time.deltaTime;
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);

    }

    IEnumerator BulletBounceAround()
    {
        float bulletSpeed = 450f;
        bulletRb = gameObject.GetComponent<Rigidbody2D>();
        bulletRb.velocity = bounceBulletDir.normalized * bulletSpeed * Time.deltaTime;
        yield return new WaitForSeconds(7f);
        Destroy(gameObject);
    }

    IEnumerator RockBullet()
    {


        float bulletSpeed = 600f;
        if(setDir == true)
        {
            rockDir = boss.rockProjDir;
            setDir = false;
        }
        else
        {

        }
        gameObject.GetComponent<Rigidbody2D>().velocity = rockDir * bulletSpeed * Time.deltaTime;
        Debug.Log(rockDir);
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("StyRock"))
        {
            if (gameObject.CompareTag("BossBouncyBullet"))
            {

            }
            else
            {
                Destroy(gameObject);
            }
            
        }

        if(other.CompareTag("WallX") && gameObject.CompareTag("BossBouncyBullet"))
        {
            bounceBulletDir.x = -bounceBulletDir.x;
        }
        if (other.CompareTag("WallY") && gameObject.CompareTag("BossBouncyBullet"))
        {
            bounceBulletDir.y = -bounceBulletDir.y;
        }
    }
}
