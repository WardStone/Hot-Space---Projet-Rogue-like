using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPartStat : MonoBehaviour
{
    public BossPatternLoop boss;

    public PlayerStat stat;
    public float partHealth;
    public bool hasRespawned = false;

    public Color hurtColor = Color.yellow;
    public Color okColor = Color.white;

    // Start is called before the first frame update
    void Start()
    {
        stat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStat>();
        boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossPatternLoop>();
        if (gameObject.CompareTag("bossLeftArm01") || gameObject.CompareTag("bossLeftArm02") || gameObject.CompareTag("bossLeftArm03"))
        {
            partHealth = 600;
        }
        if (gameObject.CompareTag("bossRightArm01") || gameObject.CompareTag("bossRightArm02") || gameObject.CompareTag("bossRightArm03"))
        {
            partHealth = 600;
        }
        if (gameObject.CompareTag("bossHead01") || gameObject.CompareTag("bossHead02") || gameObject.CompareTag("bossHead03"))
        {
            partHealth = 500;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (partHealth <= 0)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            Debug.Log("Dead");
            
        }
        else if (hasRespawned == true && partHealth > 0)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            Debug.Log("Respawned");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet") && boss.bossHealth > 750)
        {
            StartCoroutine(partTakeDamage());
            Debug.Log("PartHealt is" + partHealth);
        }
  
    }

    IEnumerator partTakeDamage()
    {
            partHealth -= stat.bulletDamage;
            boss.bossHealth -= stat.bulletDamage;
            boss.healthBar.value = boss.bossHealth;
            gameObject.GetComponent<SpriteRenderer>().material.color = hurtColor;
            yield return new WaitForSeconds(0.05f);
            gameObject.GetComponent<SpriteRenderer>().material.color = okColor;
    }
}
