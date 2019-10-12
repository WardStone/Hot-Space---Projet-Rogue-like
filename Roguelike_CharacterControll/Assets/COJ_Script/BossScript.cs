using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossScript : MonoBehaviour
{
    public int bossHealth;
    public Transform headLocation;
    public Transform rightArmLocation;
    public Transform leftArmLocation;

    public GameObject bossHead;
    public GameObject bossLeftArm;
    public GameObject bossRightArm;


    public Slider healthBar;


    void Start()
    {
        SpawnBody();
        healthBar.value = bossHealth;
    }


    void Update()
    {
        

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Projectiles"))
        {
            bossHealth -= 10;
            Debug.Log("it hits");
        }
    }
    void SpawnBody()
    {
        GameObject spawnedHead = Instantiate(bossHead, headLocation.transform.position, Quaternion.identity);
        GameObject spawnedRightArm = Instantiate(bossRightArm, rightArmLocation.transform.position, Quaternion.identity);
        GameObject spawnedleftArm = Instantiate(bossLeftArm, leftArmLocation.transform.position, Quaternion.identity);
    }



}
