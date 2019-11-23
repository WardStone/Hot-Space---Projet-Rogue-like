using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StyRockBehavior : MonoBehaviour
{
    public int health;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DmgOnFall());
    }


    IEnumerator DmgOnFall()
    {
        yield return new WaitForSeconds(0.1f);
        transform.gameObject.tag = "StyRock";
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            health -= 5;
        }

        if (other.CompareTag("EnemyBullet"))
        {
            health -= 10;
        }

        if (other.CompareTag("bossLeftArm01"))
        {
            health -= 30;
        }

        if (other.CompareTag("bossRightArm01"))
        {
            health -= 30;
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }

    }
}
