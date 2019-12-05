using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletBehaviour : MonoBehaviour
{
    public Transform playerPos;
    public Transform headPos;
    protected Vector2 homingBulletDir;
    protected Vector2 downBulletDir;
    protected Rigidbody2D bulletRb;

    public void Start()
    {
        playerPos = GameObject.Find("Player").transform;
        headPos = GameObject.Find("HeadShotPoint").transform;
        homingBulletDir = playerPos.position - headPos.position;
        downBulletDir = new Vector2(0, -1);

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

        if (gameObject.CompareTag("BossBullet"))
        {
            StartCoroutine(BulletGoesForward());
        }
    }
    IEnumerator BulletGosTowardPlayer()
    {
        float bulletSpeed = 45f;
        bulletRb = gameObject.GetComponent<Rigidbody2D>();
        bulletRb.velocity = homingBulletDir * bulletSpeed * Time.deltaTime;
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    IEnumerator BulletGoesForward()
    {
        float bulletSpeed = 150f;
        bulletRb = gameObject.GetComponent<Rigidbody2D>();
        bulletRb.velocity = downBulletDir * bulletSpeed * Time.deltaTime;
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("StyRock"))
        {
            Destroy(gameObject);
        }
    }
}
