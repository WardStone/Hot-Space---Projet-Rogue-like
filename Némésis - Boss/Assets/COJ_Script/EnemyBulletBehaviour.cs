using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletBehaviour : MonoBehaviour
{
    public Transform playerPos;
    public Transform headPos;
    protected Vector2 bulletDir;
    protected Rigidbody2D bulletRb;
    protected float bulletSpeed = 45f;

    public void Start()
    {
        playerPos = GameObject.Find("Player").transform;
        headPos = GameObject.Find("HeadPoint").transform;
        bulletDir = playerPos.position - headPos.position;
    }
    public void FixedUpdate()
    {
        StartCoroutine(BulletGosTowardPlayer());
    }

    IEnumerator BulletGosTowardPlayer()
    {
        
        bulletRb = gameObject.GetComponent<Rigidbody2D>();
        bulletRb.velocity = bulletDir * bulletSpeed * Time.deltaTime;
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
