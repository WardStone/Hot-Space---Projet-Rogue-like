using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StyRockBehavior : MonoBehaviour
{
    public int health;
    private int maxHealth;
    private Animator anim;


    private void Awake()
    {
        maxHealth = health;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DmgOnFall());
        anim = GetComponent<Animator>();
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

        else if (other.CompareTag("BossBullet") || other.CompareTag("HomingBossBullet"))
        {
            health -= 10;
        }

        else if (other.CompareTag("bossLeftArm01"))
        {
            health -= 30;
        }

        else if (other.CompareTag("bossRightArm01"))
        {
            health -= 30;
        }

        if (health <= maxHealth / 2)
        {
            anim.SetBool("switchSprite", true);
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }

    }
}
