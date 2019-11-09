using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightArm01SpawnerBehaviour : MonoBehaviour
{
    public GameObject bulletPrefab;
    protected Vector2 bulletDirection;
    protected Rigidbody2D bulletRb;
    protected GameObject bullet;
    public Transform spawnerPos;
    public float bulletSpeed = 100;
    // Start is called before the first frame update
    void Start()
    {
        bulletDirection = new Vector2(0, -1);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("bossRightArm01"))
        {
            bullet = Instantiate(bulletPrefab, spawnerPos.position, Quaternion.identity);
            bulletRb = bullet.GetComponent<Rigidbody2D>();
            StartCoroutine(bulletShot());
        }
     
    }

    IEnumerator bulletShot()
    {
        float bulletTimer = 2f;

        while (bulletTimer > 0)
        {
            bulletTimer -= Time.deltaTime;
            bulletRb.velocity = bulletDirection * Time.deltaTime * bulletSpeed;
            if (bulletRb = null)
            {
                Debug.Log("Ok niquel");
            }
            yield return null;
        }
        if (bulletTimer <= 0)
        {
            Destroy(bullet);
        }
    }
}

   

